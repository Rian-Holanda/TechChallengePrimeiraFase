using DataAccess_TechChallengePrimeiraFase.Contatos.Command;
using Entities_TechChallengePrimeiraFase.Entities;
using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Factory;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Consumidor
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private ILogger<PessoasCommand>? loggerPessoaCommand;
        public static string connectionString = $"Data Source=fiaptechchallenge.database.windows.net;Initial Catalog=FIAP_TECH_CHALLENGE;User ID=rian;Password=carvalho1991#";

        RabbitConfig rabbitConfig = new RabbitConfig();
        ApplicationDbContext _context;


        public Worker(ILogger<Worker> logger)
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
            var filas = new List<string> { "InsertPessoa", "UpdatePessoa", "ExcluirPessoa" };

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

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($" [x] Received from {fila}: {message}");

                    dynamic dataRabbit = JObject.Parse(message);
                    var teste = dataRabbit.Mensagem;
                    var pessoaEntity = JsonConvert.DeserializeObject<PessoasEntity>(teste.ToString());

                    var command = new PessoasCommand(_context, loggerPessoaCommand);

                    if (fila == "InsertPessoa")
                    {
                        command.InserirPessoa(pessoaEntity);
                    }
                    else if (fila == "UpdatePessoa")
                    {
                        command.AlterarPessoa(pessoaEntity);
                    }
                    else if (fila == "ExcluirPessoa")
                    {
                        var id = pessoaEntity.Id;
                        command.ExcluirPessoa(id);
                    }

                    Console.WriteLine($" [x] Done processing {fila}.");
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                };

                channel.BasicConsume(queue: fila, autoAck: false, consumer: consumer);
            }
            
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

    }
}

