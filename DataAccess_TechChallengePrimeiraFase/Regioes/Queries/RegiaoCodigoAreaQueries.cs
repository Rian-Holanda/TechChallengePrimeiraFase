using DataAccess_TechChallengePrimeiraFase.Regioes.Interface;
using Infrastructure_TechChallengePrimeiraFase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_TechChallengePrimeiraFase.Regioes.Queries
{
    public class RegiaoCodigoAreaQueries : IRegiaoCodigoAreaQueries
    {
        private readonly IApplicationDbContext context;
        private readonly ILogger<RegiaoCodigoAreaQueries> logger;

        public RegiaoCodigoAreaQueries(IApplicationDbContext context, ILogger<RegiaoCodigoAreaQueries> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public List<int> GetDDDs(string sigla)
        {
            try 
            { 
                var ddd = from regiao in context.Regioes
                          join regiaoCodigoArea in context.RegioesCodigosAreas on regiao.Id equals regiaoCodigoArea.IdRegiao
                          where regiao.Sigla == sigla
                          select new
                          {
                             DDD = regiaoCodigoArea.DDD
                          }.DDD;


                return ddd.ToList();
            }
            catch (Exception ex) 
            {
                logger.LogError(ex.Message);
                return new List<int>();
            }
        }

        public string? GetSiglaCodigoArea(int ddd)
        {
            try 
            {
                var resultRegiao = context.RegioesCodigosAreas
                                    .Where(rc => rc.DDD == ddd)
                                    .Include(r => r.Regiao)
                                    .First();

                return resultRegiao?.Regiao?.Sigla;
            }
            catch (Exception ex) 
            {
                logger.LogError(ex.Message);
                return String.Empty;
            }
        }

    }
}
