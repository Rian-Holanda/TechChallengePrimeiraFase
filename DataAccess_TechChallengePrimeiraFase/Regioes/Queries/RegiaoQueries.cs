﻿using DataAccess_TechChallengePrimeiraFase.Contatos.Queries;
using DataAccess_TechChallengePrimeiraFase.Regioes.Interface;
using Infrastructure_TechChallengePrimeiraFase;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_TechChallengePrimeiraFase.Regioes.Queries
{
    public class RegiaoQueries : IRegiaoQueries
    {
        private readonly IApplicationDbContext context;
        private readonly ILogger<RegiaoQueries>? logger;

        public RegiaoQueries(IApplicationDbContext context, ILogger<RegiaoQueries> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public string? GetSigla(int idRegiao) 
        {
            try 
            {
                var sigla = context.Regioes
                            .Where(r => r.Id == idRegiao)
                            .Select(r => r.Sigla)
                            .FirstOrDefault();

                return sigla;   
            }
            catch (Exception ex) 
            { 
                return String.Empty;
            }
        }
    }
}
