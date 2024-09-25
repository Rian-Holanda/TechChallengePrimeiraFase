using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Regioes.Interface
{
    public interface IRegiaoCodigoAreaProducer
    {
        bool InserirRegiaoCodigoArea(string json);
        bool AlterarRegiaoCodigoArea(string json);
        bool ExcluirRegiaoCodigoArea(int id);
    }
}
