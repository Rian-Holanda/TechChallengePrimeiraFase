using DataAccess_TechChallengePrimeiraFase.Regioes.Command;
using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Factory;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities_TechChallengePrimeiraFase.Entities;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;

namespace Consumidor_Regioes_TechChallenge
{
    internal class WorkerCodigoAreaRegiao : BackgroundService
    {
        private ILogger<RegiaoCodigoAreaCommand>? _loggerRegiaoCodigoAreaCommand;
        private readonly ILogger<WorkerCodigoAreaRegiao> _logger;
        public static string connectionString = $"Data Source=fiaptechchallenge.database.windows.net;Initial Catalog=FIAP_TECH_CHALLENGE;User ID=rian;Password=carvalho1991#";

        RabbitConfig rabbitConfig = new RabbitConfig();
        ApplicationDbContext _context;

        public WorkerCodigoAreaRegiao(ILogger<WorkerCodigoAreaRegiao> logger)
        {
            _logger = logger;

            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            builder.UseSqlServer(connectionString)
               .UseInternalServiceProvider(serviceProvider);

            _context = new ApplicationDbContext(builder.Options);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var filas = new List<string> { "InsertRegiao", "UpdateRegiao", "ExcluirRegiao" };
            RegiaoCodigoAreaCommand command = new RegiaoCodigoAreaCommand(_context, _loggerRegiaoCodigoAreaCommand);
            List<MensagemFilaRegiao> mensagensFilas = new List<MensagemFilaRegiao>();

            while (!stoppingToken.IsCancellationRequested)
            {

                var factory = rabbitConfig.Config();
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                foreach (var fila in filas)
                {

                    channel.QueueDeclare(queue: fila,
                                        durable: true,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                    var dataBody = channel.BasicGet(fila, true);

                    if (dataBody != null)
                    {

                        var message = Encoding.UTF8.GetString(dataBody.Body.ToArray());
                        Console.WriteLine($" [x] Mensagem: {message}");

                        dynamic dataRabbit = JObject.Parse(message);

                        MensagemFilaRegiao mensagemFilaRegiao = new MensagemFilaRegiao()
                        {
                            Mensagem = message,
                            Fila = fila,
                        };

                        mensagensFilas.Add(mensagemFilaRegiao);

                    }

                }

                foreach (var mensagemFila in mensagensFilas)
                {

                    if (mensagemFila.Mensagem != null && mensagemFila.Fila == "InsertRegiaoCodigoArea")
                    {

                        await command.InserirRegiaoCodigoArea(FormataRegiaoCodigoArea(mensagemFila.Mensagem));
                        Console.WriteLine("Deu certo inserir região ");
                        AckMessage(channel, mensagemFila.Fila);
                    }

                    if (mensagemFila.Mensagem != null && mensagemFila.Fila == "UpdateRegiaoCodigoArea")
                    {
                        await command.AlterarRegiaoCodigoArea(FormataRegiaoCodigoArea(mensagemFila.Mensagem));
                        Console.WriteLine("Deu certo alterar região ");
                        AckMessage(channel, mensagemFila.Fila);
                    }

                    if (mensagemFila.Mensagem != null && mensagemFila.Fila == "ExcluirRegiaoCodigoArea")
                    {
                        dynamic data = JsonConvert.DeserializeObject(mensagemFila.Mensagem);
                        var id = data.Mensagem.ToString();

                        await command.ExcluirRegiaoCodigoArea(Convert.ToInt32(id));
                        Console.WriteLine("Deu certo excluir região ");
                        AckMessage(channel, mensagemFila.Fila);

                    }
                }

                mensagensFilas.Clear();

            }
            
            


        }


        private void AckMessage(IModel channel, string fila)
        {


            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            channel.BasicConsume(queue: fila, autoAck: false, consumer: consumer);

        }


        private RegiaoCodigoAreaEntity FormataRegiaoCodigoArea(dynamic regiaoCodigoArea)
        {
            dynamic data = JsonConvert.DeserializeObject(regiaoCodigoArea);
            var objeto = data.Mensagem.ToString();


            RegiaoCodigoAreaEntity regiaoCodigoAreaEntity = JsonConvert.DeserializeObject<RegiaoCodigoAreaEntity>(objeto.ToString());

            return regiaoCodigoAreaEntity;

        }


        private int IdRegiaoCodigoArea(RegiaoCodigoAreaEntity regiaoCodigoAreaEntity)
        {
            return regiaoCodigoAreaEntity.Id;
        }

    }
}
