using DataAccess_TechChallengePrimeiraFase.Contatos.Interfaces;
using DataAccess_TechChallengePrimeiraFase.Regioes.Command;
using Infrastructure_TechChallengePrimeiraFase;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_TechChallengePrimeiraFase.Contatos.Queries
{
    public class PessoasQueries: IPessoasQueries
    {
        private readonly IApplicationDbContext context;
        private readonly ILogger<PessoasQueries> logger;

        public PessoasQueries(IApplicationDbContext context, ILogger<PessoasQueries> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public string? GetEmailPessoa(int idPessoa)
        {
            try 
            {
                var email = context.Pessoas.
                         Where(p => p.Id == idPessoa)
                        .Select(p => p.Email)
                        .FirstOrDefault();

                return (String.IsNullOrEmpty(email))?String.Empty : email;
            }
            catch(Exception ex) 
            {
                logger.LogError(ex.Message);
                return String.Empty;
            }
        }

        public string? GetNomePessoa(int idPessoa) 
        {
            try
            {
                var nome = context.Pessoas.
                         Where(p => p.Id == idPessoa)
                        .Select(p => p.Nome)
                        .FirstOrDefault();

                return (String.IsNullOrEmpty(nome)) ? String.Empty : nome;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return String.Empty;
            }
        }
    }
}
