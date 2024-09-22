using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Factory;
using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Regioes.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Regioes.Producer
{
    public class RegiaoProducer : IRegiaoProducer
    {
        MensagemRabbit mensagemRabbit = new MensagemRabbit();
        RabbitConfig rabbitConfig = new RabbitConfig();
        private readonly HttpClient _httpClient = new HttpClient();

        public bool InserirRegiao(string json)
        {
            try
            {
                bool result = mensagemRabbit.PublicarMensagem(json, "InsertRegiao");

                return result;
            }
            catch
            {
                return false;
            }
        }

        public bool AlterarRegiao(string json)
        {
            try
            {
                return mensagemRabbit.PublicarMensagem(json, "UpdateRegiao");
            }
            catch
            {
                return false;
            }
        }

        public bool ExcluirRegiao(int id)
        {
            try
            {
                return mensagemRabbit.PublicarMensagem(id.ToString(), "ExcluirRegiao");
            }
            catch
            {
                return false;
            }
        }
    }
}
