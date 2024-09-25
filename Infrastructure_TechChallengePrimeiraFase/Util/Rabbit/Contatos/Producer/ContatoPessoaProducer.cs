using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Contatos.Interface;
using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Contatos.Producer
{
    public class ContatoPessoaProducer : IContatoPessoaProducer
    {
        MensagemRabbit mensagemRabbit = new MensagemRabbit();
        RabbitConfig rabbitConfig = new RabbitConfig();
        private readonly HttpClient _httpClient = new HttpClient();

        public bool InserirContatoPessoa(string json)
        {
            try
            {
                bool result = mensagemRabbit.PublicarMensagem(json, "InsertContatoPessoa");

                return result;
            }
            catch
            {
                return false;
            }
        }

        public bool AlterarContatoPessoa(string json)
        {
            try
            {
                return mensagemRabbit.PublicarMensagem(json, "UpdateContatoPessoa");
            }
            catch
            {
                return false;
            }
        }

        public bool ExcluirContatoPessoa(int id)
        {
            try
            {
                return mensagemRabbit.PublicarMensagem(id.ToString(), "ExcluirContatoPessoa");
            }
            catch
            {
                return false;
            }
        }
    }
}
