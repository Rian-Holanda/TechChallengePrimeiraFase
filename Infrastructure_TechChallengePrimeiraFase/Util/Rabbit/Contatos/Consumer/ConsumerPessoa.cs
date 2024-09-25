using Entities_TechChallengePrimeiraFase.Entities;
using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Factory;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;


namespace Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Contatos.Consumer
{
    public class ConsumerPessoa
    {
        RabbitConfig rabbitConfig = new RabbitConfig();
        private readonly HttpClient _httpClient = new HttpClient();
        //private readonly PessoasCommand pessoasCommand = new PessoasCommand();
        public async Task<string> InsertPessoa(PessoasEntity entity)
        {
            string messageConsumer = string.Empty;

            try
            {


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
