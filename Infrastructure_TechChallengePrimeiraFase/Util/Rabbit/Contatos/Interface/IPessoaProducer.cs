using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Contatos.Interface
{
    public interface IPessoaProducer
    {
        bool InserirPessoa(string json);
        bool AlterarPessoa(string json);
        bool ExcluirPessoa(string json);

    }
}
