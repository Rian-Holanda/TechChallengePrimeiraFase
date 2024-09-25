using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Factory;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_TechChallengePrimeiraFase.Util.Rabbit
{
    public class MensagemRabbit
    {

        RabbitConfig rabbitConfig = new RabbitConfig();

        public bool PublicarMensagem(string mensagem, string nomeFila)
        {
            try
            {
                using (var connection = rabbitConfig.Config().CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(
                                queue: nomeFila,
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

                        var message = new { Mensagem = mensagem };
                        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                        channel.BasicPublish(exchange: "", routingKey: nomeFila, body: body);

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
