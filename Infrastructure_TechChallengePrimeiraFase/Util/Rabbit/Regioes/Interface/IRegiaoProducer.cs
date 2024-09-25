using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Regioes.Interface
{
    public interface IRegiaoProducer
    {
        bool InserirRegiao(string json);
        bool AlterarRegiao(string json);
        bool ExcluirRegiao(int id);
    }
}
