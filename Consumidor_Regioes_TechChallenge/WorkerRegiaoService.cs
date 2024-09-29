using DataAccess_TechChallengePrimeiraFase.Contatos.Command;
using DataAccess_TechChallengePrimeiraFase.Regioes.Command;
using DataAccess_TechChallengePrimeiraFase.Regioes.Interface;
using Entities_TechChallengePrimeiraFase.Entities;
using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Factory;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Consumidor_Regioes_TechChallenge
{
    public class WorkerRegiaoService : BackgroundService
    {

        private readonly ILogger<WorkerRegiaoService> _logger;
        private readonly IServiceProvider _serviceProvider;
        RabbitConfig rabbitConfig = new RabbitConfig();


        public WorkerRegiaoService(ILogger<WorkerRegiaoService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope()) 
            {
                var regiaoCommand  = scope.ServiceProvider.GetRequiredService<IRegiaoCommand>();
                var regiaoCodigoAreaCommand = scope.ServiceProvider.GetRequiredService<IRegiaoCodigoAreaCommand>();

                var filas = new List<string> { "InsertRegiao",
                                           "UpdateRegiao",
                                           "ExcluirRegiao",
                                           "InsertRegiaoCodigoArea",
                                           "UpdateRegiaoCodigoArea",
                                           "ExcluirRegiaoCodigoArea" };


                while (!stoppingToken.IsCancellationRequested)
                {

                    List<MensagemFilaRegiao> mensagensFilas = new List<MensagemFilaRegiao>();

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
                            var result = await regiaoCommand.InserirRegiao(FormataRegiao(mensagemFila.Mensagem)) > 0? "Deu certo inserir região ":"ERRO";
                            Console.WriteLine(result);

                            if (result != "ERRO")
                                AckMessage(channel, mensagemFila.Fila);

                        }

                        if (mensagemFila.Mensagem != null && mensagemFila.Fila == "UpdateRegiao")
                        {

                            var regiao = FormataRegiao(mensagemFila.Mensagem);
                            var id = IdRegiao(regiao);
                            var result = await regiaoCommand.AlterarRegiao(regiao, id) ? "Deu certo excluir região " : "ERRO";
                            Console.WriteLine("Deu certo alterar região ");
                            
                            if (result != "ERRO")
                                AckMessage(channel, mensagemFila.Fila);

                        }

                        if (mensagemFila.Mensagem != null && mensagemFila.Fila == "ExcluirRegiao")
                        {
                            dynamic data = JsonConvert.DeserializeObject(mensagemFila.Mensagem);
                            var id = data.Mensagem.ToString();
                            var result = await regiaoCommand.ExcluirRegiao(Convert.ToInt32(id))? "Deu certo excluir região ": "ERRO";
                            Console.WriteLine(result);

                            if(result != "ERRO")
                                AckMessage(channel, mensagemFila.Fila);

                        }


                        if (mensagemFila.Mensagem != null && mensagemFila.Fila == "InsertRegiaoCodigoArea")
                        {
                            var regiaoCodigoArea = FormataRegiaoCodigoArea(mensagemFila.Mensagem);
                            var result = await regiaoCodigoAreaCommand.InserirRegiaoCodigoArea(regiaoCodigoArea) > 0? "Deu certo inserir código de área ": "ERRO";
                            Console.WriteLine(result);

                            if (result != "ERRO")
                                AckMessage(channel, mensagemFila.Fila);
                        }

                        if (mensagemFila.Mensagem != null && mensagemFila.Fila == "UpdateRegiaoCodigoArea")
                        {

                            var regiaoCodigoArea = FormataRegiaoCodigoArea(mensagemFila.Mensagem);
                            var id = IdRegiaoCodigoArea(regiaoCodigoArea);
                            var result = await regiaoCodigoAreaCommand.AlterarRegiaoCodigoArea(regiaoCodigoArea,id)? "Deu certo alterar código de área ":"ERRO";
                            Console.WriteLine(result);
                            
                            if(result != "ERRO")
                                AckMessage(channel, mensagemFila.Fila);
                        }

                        if (mensagemFila.Mensagem != null && mensagemFila.Fila == "ExcluirRegiaoCodigoArea")
                        {
                            dynamic data = JsonConvert.DeserializeObject(mensagemFila.Mensagem);
                            var id = data.Mensagem.ToString();
                            var result = await regiaoCodigoAreaCommand.ExcluirRegiaoCodigoArea(Convert.ToInt32(id))? "Deu certo excluir código de área ": "ERRO";
                            Console.WriteLine(result);

                            if (result != "ERRO")
                                AckMessage(channel, mensagemFila.Fila);
                        }
                    }

                    mensagensFilas.Clear();

                }
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


        private RegiaoEntity FormataRegiao(dynamic regiao)
        {
            dynamic data = JsonConvert.DeserializeObject(regiao);
            var objeto = data.Mensagem.ToString();


            RegiaoEntity regiaoEntity = JsonConvert.DeserializeObject<RegiaoEntity>(objeto.ToString());

            return regiaoEntity;

        }

        private int IdRegiao(RegiaoEntity regioesEntity)
        {
            return regioesEntity.Id;
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

        private string Sigla(RegiaoEntity regioesEntity)
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

