using Business_TechChallengePrimeiraFase.Regioes.Application.Interfaces;
using DataAccess_TechChallengePrimeiraFase.Regioes.Interface;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_TechChallengePrimeiraFase.Regioes.Domain
{
    public class ValidacoesRegioes : IValidacoesRegioes
    {
        private readonly ILogger _logger;
        private readonly IRegiaoCodigoAreaQueries _regiaoCodigoAreaQueries;
        private readonly IRegiaoQueries _regiaoQueries;
        private readonly IDatabase _db;

        public ValidacoesRegioes(ILogger logger, IDatabase db, IRegiaoCodigoAreaQueries regiaoCodigoAreaQueries, IRegiaoQueries regiaoQueries)
        {
            _logger = logger;
            _db = db;
            _regiaoCodigoAreaQueries = regiaoCodigoAreaQueries;
            _regiaoQueries = regiaoQueries;
        }


        public bool ValidaRegiaoExistente(string sigla) 
        {
            try 
            {
               var regiao =  _regiaoQueries.GetRegiaoExistente(sigla);

                return (regiao is not null);
            }
            catch 
            {
                return false;
            }
        }

        public bool ValidaCodigoAreaExistente(int ddd) 
        {
            try
            {
                var validacao = _regiaoCodigoAreaQueries.GetSiglaCodigoArea(ddd);

                return (!String.IsNullOrEmpty(validacao));
            }
            catch
            {
                return false;
            }
        }
    }
}
