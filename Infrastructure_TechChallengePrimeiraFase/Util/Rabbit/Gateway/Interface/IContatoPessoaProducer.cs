using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Gateway.Interface
{
    public interface IContatoPessoaProducer
    {
        bool InserirContatoPessoa(string json);
        bool AlterarContatoPessoa(string json);
        bool ExcluirContatoPessoa(int id);

    }
}
