using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Factory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Contatos.Consumer
{
    public class ConsumerPessoa
    {
        RabbitConfig rabbitConfig = new RabbitConfig();
        private readonly HttpClient _httpClient = new HttpClient();
        public async Task<string> InsertPessoa(string guid)
        {
            string messageConsumer = string.Empty;

            try
            {
                var factory = rabbitConfig.Config();
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(
                                queue: "Pessoas",
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

                var data = channel.BasicGet("Pessoas", false);
                messageConsumer = Encoding.UTF8.GetString(data.Body.ToArray()).ToString();

                dynamic dataRabbit = JObject.Parse(messageConsumer);
                var ticket = dataRabbit["Ticket"].Value;

                if (guid == ticket)
                {
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (ch, ea) =>
                    {
                        channel.BasicAck(ea.DeliveryTag, false);
                    };
                }

                return messageConsumer;
            }

            catch (Exception ex)
            {
                return "";
            }

        }

        public bool StatusInserirPessoa(string json)
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

                        var message = new { Mensagem = json};
                        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                        channel.BasicPublish(exchange: "", routingKey: "Pessoas", body: body);

                        var insert = _httpClient.GetAsync("https://localhost:44345/Pessoa/GetPessoasProducer");

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
