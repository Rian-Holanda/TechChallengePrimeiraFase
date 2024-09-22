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
                var pessoas = await _httpClient.GetFromJsonAsync<List<Pessoa>>("https://localhost:44343/GetPessoas");

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
                bool result =  PublicarMensagem(json);

                return  result;
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
                return PublicarMensagem(json);
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
                return PublicarMensagem(id.ToString());
            }
            catch
            {
                return false;
            }
        }

        public bool PublicarMensagem (string mensagem) 
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

                        var message = new { Mensagem = mensagem };
                        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                        channel.BasicPublish(exchange: "", routingKey: "Pessoas", body: body);

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
