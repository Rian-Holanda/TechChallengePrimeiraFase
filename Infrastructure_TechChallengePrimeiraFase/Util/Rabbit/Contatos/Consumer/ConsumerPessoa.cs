using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Factory;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Contatos.Consumer
{
    public class ConsumerPessoa
    {
        RabbitConfig rabbitConfig = new RabbitConfig();
        private readonly HttpClient _httpClient = new HttpClient();
        public string InsertPessoa(string guid) 
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

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (ch, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    channel.BasicAck(ea.DeliveryTag, false);
                };
                // this consumer tag identifies the subscription
                // when it has to be cancelled
                var data = channel.BasicGet("Pessoas", false);
                return messageConsumer  = Encoding.UTF8.GetString(data.Body.ToArray()).ToString();
            }


            catch (Exception ex) 
            {
                return "";
            }
            
        }

    }
}
