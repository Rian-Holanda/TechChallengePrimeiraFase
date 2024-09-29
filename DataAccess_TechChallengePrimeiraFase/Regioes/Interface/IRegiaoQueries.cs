using Entities_TechChallengePrimeiraFase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_TechChallengePrimeiraFase.Regioes.Interface
{
    public interface IRegiaoQueries
    {
        string? GetSigla(int idRegiao);

        RegiaoEntity GetRegiaoExistente(string? sigla);
    }
}
