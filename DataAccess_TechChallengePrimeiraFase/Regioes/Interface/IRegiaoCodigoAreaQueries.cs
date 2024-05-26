using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_TechChallengePrimeiraFase.Regioes.Interface
{
    public interface IRegiaoCodigoAreaQueries
    {
        List<int> GetDDDs(string sigla);

        string? GetSiglaCodigoArea(int ddd);
    }
}
