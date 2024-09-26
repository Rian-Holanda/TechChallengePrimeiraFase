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

        Task<int> InserirRegiaoCodigoArea(RegioesCodigosAreasEntity regioesCodigosAreasEntity);
        Task<bool> AlterarRegiaoCodigoArea(RegioesCodigosAreasEntity regioesEntity, int idRidRegiaoCodigoAreaegiao);
        Task<bool> ExcluirRegiaoCodigoArea(int idRegiaoCodigoArea);
        RegioesCodigosAreasEntity? GetRegiaoCodigoArea(int idRegiaoCodigoArea);
        List<RegioesCodigosAreasEntity>? GetRegioesCodigosAreas();
    }
}
