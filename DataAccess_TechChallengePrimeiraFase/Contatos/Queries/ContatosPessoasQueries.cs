using DataAccess_TechChallengePrimeiraFase.Contatos.Interfaces;
using Infrastructure_TechChallengePrimeiraFase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataAccess_TechChallengePrimeiraFase.Contatos.Queries
{
    public class ContatosPessoasQueries : IContatosPessoasQueries
    {
        private readonly IApplicationDbContext context;
        private readonly ILogger<ContatosPessoasQueries>? logger;

        public ContatosPessoasQueries(IApplicationDbContext context, ILogger<ContatosPessoasQueries> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public string? GetRegiaoContatoPessoa(string nomePessoa, string numeroPessoa)
        {
            try
            {
                var query  = from pessoa in context.Pessoas
                             join contato in context.ContatosPessoas on pessoa.Id equals contato.IdPessoa
                             join regiao in context.Regioes on contato.IdRegiao equals regiao.Id
                             where contato.Numero == numeroPessoa && pessoa.Nome == nomePessoa
                             select new
                             {
                                 Sigla = regiao.Sigla
                             }.Sigla;

                var regiaoSigla = query.FirstOrDefault();

                return regiaoSigla;
                                              
            }
            catch (Exception ex) 
            { 
                return String.Empty;
            }
        }
    }
}
