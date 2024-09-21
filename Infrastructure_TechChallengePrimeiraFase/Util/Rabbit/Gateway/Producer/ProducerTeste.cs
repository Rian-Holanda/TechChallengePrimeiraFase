using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Factory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Gateway.Producer
{
    public class ProducerTeste
    {
        RabbitConfig rabbitConfig = new RabbitConfig();

        public bool TesteConexaoRabbitPublish(string mensagem)
        {
            var factory = rabbitConfig.Config();

            try
            {

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(
                                queue: "FIAP",
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

                        var message = new { Name = "FIAP", Message = mensagem };
                        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                        channel.BasicPublish(exchange: "", routingKey: "FIAP", body: body);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }

        public string TesteConexaoRabbitConsume()
        {
            var factory = rabbitConfig.Config();
            string mensagem = "";

            try
            {

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += (sender, eventArgs) =>
                        {
                            var body = eventArgs.Body.ToArray();
                            mensagem = Encoding.UTF8.GetString(body);
                        };

                        channel.BasicConsume(queue: "FIAP", autoAck: true, consumer: consumer);

                    }
                }

                var teste = JsonConvert.DeserializeObject(mensagem);
                dynamic data = JObject.Parse(mensagem);


                return data.Message.ToString();
            }
            catch (Exception ex)
            {
                return "";

            }
        }
    }
}
