using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities_TechChallengePrimeiraFase.Entities;

namespace DataAccess_TechChallengePrimeiraFase.Regioes.Interface
{
    public interface IRegiaoCodigoAreaCommand
    {

        Task<int> InserirRegiaoCodigoArea(RegiaoCodigoAreaEntity regioesCodigosAreasEntity);
        Task<bool> AlterarRegiaoCodigoArea(RegiaoCodigoAreaEntity regioesCodigosAreasEntity, int id);
        Task<bool> ExcluirRegiaoCodigoArea(int idRegiaoCodigoArea);
        RegiaoCodigoAreaEntity? GetRegiaoCodigoArea(int idRegiaoCodigoArea);
        List<RegiaoCodigoAreaEntity>? GetRegioesCodigosAreas();
    }
}
