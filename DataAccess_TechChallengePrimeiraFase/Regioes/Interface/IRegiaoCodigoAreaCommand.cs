using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities_TechChallengePrimeiraFase.Entities;

namespace DataAccess_TechChallengePrimeiraFase.Regioes.Interface
{
    internal interface IRegiaoCodigoAreaCommand
    {

        int InserirRegiaoCodigoArea(RegioesCodigosAreasEntity regioesCodigosAreasEntity);
        bool AlterarRegiaoCodigoArea(RegioesCodigosAreasEntity regioesEntity, int idRidRegiaoCodigoAreaegiao);
        bool ExcluirRegiaoCodigoArea(int idRegiaoCodigoArea);
        RegioesCodigosAreasEntity? GetRegiaoCodigoArea(int idRegiaoCodigoArea);
        List<RegioesCodigosAreasEntity>? GetRegioesCodigosAreas();
    }
}
