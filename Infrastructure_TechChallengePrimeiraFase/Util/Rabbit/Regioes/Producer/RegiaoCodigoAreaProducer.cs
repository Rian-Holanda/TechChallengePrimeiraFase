using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Factory;
using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Regioes.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Regioes.Producer
{
    public class RegiaoCodigoAreaProducer : IRegiaoCodigoAreaProducer
    {

        MensagemRabbit mensagemRabbit = new MensagemRabbit();
        RabbitConfig rabbitConfig = new RabbitConfig();
        private readonly HttpClient _httpClient = new HttpClient();

        public bool InserirRegiaoCodigoArea(string json)
        {
            try
            {
                bool result = mensagemRabbit.PublicarMensagem(json, "InsertRegiaoCodigoArea");

                return result;
            }
            catch
            {
                return false;
            }
        }

        public bool AlterarRegiaoCodigoArea(string json)
        {
            try
            {
                return mensagemRabbit.PublicarMensagem(json, "UpdateRegiaoCodigoArea");
            }
            catch
            {
                return false;
            }
        }

        public bool ExcluirRegiaoCodigoArea(int id)
        {
            try
            {
                return mensagemRabbit.PublicarMensagem(id.ToString(), "ExcluirRegiaoCodigoArea");
            }
            catch
            {
                return false;
            }
        }
    }
}
