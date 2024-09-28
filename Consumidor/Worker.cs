using API_Producer_TechChallenge.Models.Pessoa;
using DataAccess_TechChallengePrimeiraFase.Contatos.Command;
using DataAccess_TechChallengePrimeiraFase.Contatos.Interface;
using DataAccess_TechChallengePrimeiraFase.Contatos.Queries;
using Entities_TechChallengePrimeiraFase.Entities;
using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Contatos.Consumer;
using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Factory;
using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Gateway;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var filas = new List<string> { "InsertPessoa" };
            PessoasCommand command = new PessoasCommand(_context, loggerPessoaCommand);
          

            while (!stoppingToken.IsCancellationRequested)
            {
                var factory = rabbitConfig.Config();
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                foreach (var fila in filas) {

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

                        //int dots = message.Split('.').Length - 1;
                        //Thread.Sleep(dots * 1000);


                        dynamic dataRabbit = JObject.Parse(message);

                        var teste = dataRabbit.Mensagem;

                        var pessoaEntity = JsonConvert.DeserializeObject<PessoaEntity>(teste.ToString());

                        switch (fila)
                        {
                            case "InsertPessoa":
                                command.InserirPessoa(pessoaEntity); ;
                                break;
                        }

                        Console.WriteLine(" [x] Done");

                        // here channel could also be accessed as ((EventingBasicConsumer)sender).Model
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    };
                    channel.BasicConsume(queue: fila,
                                         autoAck: false,
                                         consumer: consumer);

                    //Console.WriteLine(" Press [enter] to exit.");
                    //Console.ReadLine();

                }   

            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}

