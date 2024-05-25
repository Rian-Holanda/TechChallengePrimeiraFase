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
        private readonly ILogger<RegiaoCodigoAreaQueries>? logger;

        public RegiaoCodigoAreaQueries(IApplicationDbContext context, ILogger<RegiaoCodigoAreaQueries> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public int? GetDDD(int idRegiao)
        {
            try 
            { 
                var ddd = from regiao in context.Regioes
                          join regiaoCodigoArea in context.RegioesCodigosAreas on regiao.Id equals regiaoCodigoArea.IdRegiao
                          where regiao.Id == idRegiao
                          select new
                          {
                             DDD = regiaoCodigoArea.DDD
                          }.DDD;


                return ddd.First();
            }
            catch (Exception ex) 
            {
                return 0;
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
                return String.Empty;
            }
        }

    }
}
