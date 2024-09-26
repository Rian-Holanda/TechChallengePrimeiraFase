using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Contatos.Interface;
using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Factory;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Contatos.Producer
{
    public class PessoaProducer : IPessoaProducer
    {
        MensagemRabbit mensagemRabbit = new MensagemRabbit();
        RabbitConfig rabbitConfig = new RabbitConfig();
        private readonly HttpClient _httpClient = new HttpClient();

        public bool InserirPessoa(string json)
        {
            try
            {
                bool result = mensagemRabbit.PublicarMensagem(json, "InsertPessoa");

                return result;
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
                return mensagemRabbit.PublicarMensagem(json, "UpdatePessoa");
            }
            catch
            {
                return false;
            }
        }

        public bool ExcluirPessoa(string json)
        {
            try
            {
                return mensagemRabbit.PublicarMensagem(json, "ExcluirPessoa");
            }
            catch
            {
                return false;
            }
        }
    }
}
