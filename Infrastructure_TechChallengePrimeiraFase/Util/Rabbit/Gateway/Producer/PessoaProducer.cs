using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Factory;
using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Gateway.Interface;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Gateway.Producer
{
    public class PessoaProducer : IPessoaProducer
    {
        RabbitConfig rabbitConfig = new RabbitConfig();
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<List<Pessoa>> GetPessoas()
        {
            try
            {
                var pessoas = await _httpClient.GetFromJsonAsync<List<Pessoa>>("https://localhost:44343/GetPessoasConsumer");

                if (pessoas?.Count > 0)
                {
                    return pessoas;
                }
                else 
                {
                    return new List<Pessoa>();
                }

            }
            catch
            {
                return new List<Pessoa>();
            }
        }
        public async Task<Pessoa> GetPessoa(int id)
        {
            try
            {
                var pessoa = await _httpClient.GetFromJsonAsync<Pessoa>("https://localhost:44343/GetPessoa");

                if (pessoa is not null )
                {
                    return pessoa;
                }
                else
                {
                    return new Pessoa();
                }
            }
            catch
            {
                return new Pessoa();
            }
        }

        public bool InserirPessoa(string json)
        {
            try
            {
                var guid = Guid.NewGuid();

                using (var connection = rabbitConfig.Config().CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(
                                queue: "Pessoas",
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

                        var message = new { Objeto = json, Mensagem = "InserirPessoa", Ticket = guid };
                        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                        channel.BasicPublish(exchange: "", routingKey: "Pessoas", body: body);

                        var insert =  _httpClient.PostAsJsonAsync("https://localhost:44343/InserirPessoa", guid.ToString() );

                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool AlterarPessoa(string json)
        {
            try
            {
                using (var connection = rabbitConfig.Config().CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(
                                queue: "Pessoas",
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

                        var message = new { Objeto = json, Mensagem = "AlterarPessoa" };
                        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                        channel.BasicPublish(exchange: "", routingKey: "Pessoas", body: body);

                        var insert =  _httpClient.PostAsJsonAsync("https://localhost:44343/AlterarPessoa", body);

                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool ExcluirPessoa(int id)
        {
            try
            {
                using (var connection = rabbitConfig.Config().CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(
                                queue: "Pessoas",
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

                        var message = new { IdPessoa = id.ToString(), Mensagem = "InserirPessoa" };
                        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                        channel.BasicPublish(exchange: "", routingKey: "Pessoas", body: body);

                        var insert =  _httpClient.PostAsJsonAsync("https://localhost:44343/AlterarPessoa", id);

                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
