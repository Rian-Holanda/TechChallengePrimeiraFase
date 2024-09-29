using API_Producer_TechChallenge.Models.Pessoa;
using DataAccess_TechChallengePrimeiraFase.Contatos.Command;
using DataAccess_TechChallengePrimeiraFase.Contatos.Interface;
using DataAccess_TechChallengePrimeiraFase.Contatos.Queries;
using DataAccess_TechChallengePrimeiraFase.Regioes.Interface;
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
    public class WorkerPessoaService : BackgroundService
    {

        private readonly ILogger<WorkerPessoaService> _logger;
        private readonly IServiceProvider _serviceProvider;
        RabbitConfig rabbitConfig = new RabbitConfig();


        public WorkerPessoaService(ILogger<WorkerPessoaService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var pessoaCommand = scope.ServiceProvider.GetRequiredService<IPessoasCommand>();
                var contatoPessoaCommand = scope.ServiceProvider.GetRequiredService<IContatosPessoasCommand>();

                var filas = new List<string> { "InsertPessoa",
                                           "UpdatePessoa",
                                           "ExcluirPessoa",
                                           "InsertContatoPessoa",
                                           "UpdateContatoPessoa",
                                           "ExcluirContatoPessoa" };


                while (!stoppingToken.IsCancellationRequested)
                {

                    List<MensagemFilaPessoa> mensagensFilas = new List<MensagemFilaPessoa>();

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

                            MensagemFilaPessoa mensagemFilaPessoa = new MensagemFilaPessoa()
                            {
                                Mensagem = message,
                                Fila = fila,
                            };

                            mensagensFilas.Add(mensagemFilaPessoa);

                        }

                    }

                    foreach (var mensagemFila in mensagensFilas)
                    {

                        if (mensagemFila.Mensagem != null && mensagemFila.Fila == "InsertPessoa")
                        {
                            var result = await pessoaCommand.InserirPessoa(FormataPessoa(mensagemFila.Mensagem)) > 0 ? "Deu certo inserir pessoa " : "ERRO";
                            Console.WriteLine(result);

                            if (result != "ERRO")
                                AckMessage(channel, mensagemFila.Fila);

                        }

                        if (mensagemFila.Mensagem != null && mensagemFila.Fila == "UpdatePessoa")
                        {

                            var pessoa = FormataPessoa(mensagemFila.Mensagem);
                            var id = IdPessoa(pessoa);
                            var result = await pessoaCommand.AlterarPessoa(pessoa, id) ? "Deu certo alterar pessoa " : "ERRO";
                            Console.WriteLine("Deu certo alterar região ");

                            if (result != "ERRO")
                                AckMessage(channel, mensagemFila.Fila);

                        }

                        if (mensagemFila.Mensagem != null && mensagemFila.Fila == "ExcluirPessoa")
                        {
                            dynamic data = JsonConvert.DeserializeObject(mensagemFila.Mensagem);
                            var id = data.Mensagem.ToString();
                            var result = await pessoaCommand.ExcluirPessoa(Convert.ToInt32(id)) ? "Deu certo excluir pessoa " : "ERRO";
                            Console.WriteLine(result);

                            if (result != "ERRO")
                                AckMessage(channel, mensagemFila.Fila);

                        }


                        if (mensagemFila.Mensagem != null && mensagemFila.Fila == "InsertContatoPessoa")
                        {
                            var contatoPessoa = FormataContatoPessoa(mensagemFila.Mensagem);
                            var result = await contatoPessoaCommand.InserirContatoPessoa(contatoPessoa) > 0 ? "Deu certo inserir contato pessoa" : "ERRO";
                            Console.WriteLine(result);

                            if (result != "ERRO")
                                AckMessage(channel, mensagemFila.Fila);
                        }

                        if (mensagemFila.Mensagem != null && mensagemFila.Fila == "UpdateContatoPessoa")
                        {

                            var contatoPessoa = FormataContatoPessoa(mensagemFila.Mensagem);
                            var id = IdContatoPessoa(contatoPessoa);
                            var result = await contatoPessoaCommand.AlterarContatoPessoa(contatoPessoa, id) ? "Deu certo alterar contato pessoa" : "ERRO";
                            Console.WriteLine(result);

                            if (result != "ERRO")
                                AckMessage(channel, mensagemFila.Fila);
                        }

                        if (mensagemFila.Mensagem != null && mensagemFila.Fila == "ExcluirContatoPessoa")
                        {
                            dynamic data = JsonConvert.DeserializeObject(mensagemFila.Mensagem);
                            var id = data.Mensagem.ToString();
                            var result = await contatoPessoaCommand.ExcluirContatoPessoa(Convert.ToInt32(id)) ? "Deu certo excluir código de área " : "ERRO";
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


        private PessoaEntity FormataPessoa(dynamic regiao)
        {
            dynamic data = JsonConvert.DeserializeObject(regiao);
            var objeto = data.Mensagem.ToString();


            PessoaEntity pessoaEntity = JsonConvert.DeserializeObject<PessoaEntity>(objeto.ToString());

            return pessoaEntity;

        }

        private int IdPessoa(PessoaEntity pessoaEntity)
        {
            return pessoaEntity.Id;
        }

        private ContatoPessoaEntity FormataContatoPessoa(dynamic contatoPessoa)
        {
            dynamic data = JsonConvert.DeserializeObject(contatoPessoa);
            var objeto = data.Mensagem.ToString();

            ContatoPessoaEntity contatoPessoaEntity = JsonConvert.DeserializeObject<ContatoPessoaEntity>(objeto.ToString());

            return contatoPessoaEntity;

        }

        private int IdContatoPessoa(ContatoPessoaEntity contatoPessoaEntity)
        {
            return contatoPessoaEntity.Id;
        }


    }
}




public class MensagemFilaPessoa
{
    public string? Mensagem { get; set; }
    public string? Fila { get; set; }

}