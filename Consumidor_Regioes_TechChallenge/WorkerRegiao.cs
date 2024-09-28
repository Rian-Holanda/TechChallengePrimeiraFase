using DataAccess_TechChallengePrimeiraFase.Contatos.Command;
using DataAccess_TechChallengePrimeiraFase.Regioes.Command;
using Entities_TechChallengePrimeiraFase.Entities;
using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Factory;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace Consumidor_Regioes_TechChallenge
{
    public class WorkerRegiao : BackgroundService
    {

        private ILogger<RegiaoCommand>? loggerRegiaoCommand;
        public static string connectionString = $"Data Source=fiaptechchallenge.database.windows.net;Initial Catalog=FIAP_TECH_CHALLENGE;User ID=rian;Password=carvalho1991#";

        RabbitConfig rabbitConfig = new RabbitConfig();
        ApplicationDbContext _context;


        private readonly ILogger<WorkerRegiao> _logger;

        public WorkerRegiao(ILogger<WorkerRegiao> logger)
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
            RegiaoCommand command = new RegiaoCommand(_context, loggerRegiaoCommand);
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

                    if (mensagemFila.Mensagem != null && mensagemFila.Fila == "InsertRegiao")
                    {

                        await command.InserirRegiao(FormataRegiao(mensagemFila.Mensagem));
                        Console.WriteLine("Deu certo inserir região ");
                        AckMessage(channel, mensagemFila.Fila);
                    }

                    if (mensagemFila.Mensagem != null && mensagemFila.Fila == "UpdateRegiao")
                    {
                        await command.AlterarRegiao(FormataRegiao(mensagemFila.Mensagem));
                        Console.WriteLine("Deu certo alterar região ");
                        AckMessage(channel, mensagemFila.Fila);
                    }

                    if (mensagemFila.Mensagem != null && mensagemFila.Fila == "ExcluirRegiao")
                    {
                        dynamic data = JsonConvert.DeserializeObject(mensagemFila.Mensagem);
                        var id = data.Mensagem.ToString();

                        await command.ExcluirRegiao(Convert.ToInt32(id));
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


        private RegioesEntity FormataRegiao(dynamic regiao)
        {
            dynamic data = JsonConvert.DeserializeObject(regiao);
            var objeto = data.Mensagem.ToString();


            RegioesEntity regiaoEntity = JsonConvert.DeserializeObject<RegioesEntity>(objeto.ToString());

            return regiaoEntity;

        }


        private int IdRegiao(RegioesEntity regioesEntity)
        {
            return regioesEntity.Id;
        }

        private string Sigla(RegioesEntity regioesEntity)
        {
            return regioesEntity.Sigla;
        }


    }

}




public class MensagemFilaRegiao
{
    public string? Mensagem { get; set; }
    public string? Fila { get; set; }

}

