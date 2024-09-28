using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities_TechChallengePrimeiraFase.Entities;

namespace DataAccess_TechChallengePrimeiraFase.Regioes.Interface
{
    public interface IRegiaoCommand
    {

        Task<int> InserirRegiao(RegiaoEntity regioesEntity);
        Task<bool> AlterarRegiao(RegiaoEntity regioesEntity);
        Task<bool> ExcluirRegiao(int idRegiao);
        RegiaoEntity? GetRegiao(int idRegiao);
        List<RegiaoEntity>? GetRegioes();
    }
}
