using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_TechChallengePrimeiraFase.Regioes.Application.Interfaces
{
    public interface IValidacoesRegioes
    {
        public bool ValidaRegiaoExistente(string sigla);

        public bool ValidaCodigoAreaExistente(int ddd);
    }
}
