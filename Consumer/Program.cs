using DataAccess_TechChallengePrimeiraFase.Contatos.Command;
using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Factory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

class Program
{
    private static readonly ILogger<Program> _logger;
    private static ILogger<PessoasCommand>? loggerPessoaCommand;
    public static string connectionString = $"Data Source=fiaptechchallenge.database.windows.net;Initial Catalog=FIAP_TECH_CHALLENGE;User ID=rian;Password=carvalho1991#";
    private static RabbitConfig rabbitConfig = new RabbitConfig();
    private static ApplicationDbContext _context;

    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkSqlServer()
            .BuildServiceProvider();

        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

        builder.UseSqlServer(connectionString)
            .UseInternalServiceProvider(serviceProvider);

        _context = new ApplicationDbContext(builder.Options);

        var filas = new List<string> { "UpdatePessoa" };

        foreach (var fila in filas)
        {
            PessoasCommand command = new PessoasCommand(_context, loggerPessoaCommand);

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Port = 5672,
                Password = "guest"


            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: fila,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($" [x] Received {message}");

                int dots = message.Split('.').Length - 1;
                Thread.Sleep(dots * 1000);

                Console.WriteLine(" [x] Done");

                // here channel could also be accessed as ((EventingBasicConsumer)sender).Model
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            channel.BasicConsume(queue: fila,
                                 autoAck: false,
                                 consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

        }

    }      

}
