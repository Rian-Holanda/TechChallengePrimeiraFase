using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess_TechChallengePrimeiraFase.Contatos.Interface;
using DataAccess_TechChallengePrimeiraFase.Regioes.Interface;
using Entities_TechChallengePrimeiraFase.Entities;
using Infrastructure_TechChallengePrimeiraFase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess_TechChallengePrimeiraFase.Regioes.Command
{
    public class ContatosPessoasCommand : IContatosPessoasCommand
    {
        private readonly IApplicationDbContext context;
        private readonly ILogger<ContatosPessoasCommand>? logger;

        public ContatosPessoasCommand( IApplicationDbContext context, ILogger<ContatosPessoasCommand>? logger)
        {
            this.context = context;
            this.logger = logger;
        }


        public async Task<int> InserirContatoPessoa(ContatoPessoaEntity contatoPessoaEntity)
        {
            try 
            {
                return await context.ContatosPessoas.Add(contatoPessoaEntity).Context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                logger?.LogError(ex.Message);
                return 0;
            }
        }

        public async Task<bool> AlterarContatoPessoa(ContatoPessoaEntity contatoPessoaEntity, int idContatoPessoa)
        {
            try 
            {
                
                var result = await context.ContatosPessoas.Where(cp => cp.Id == idContatoPessoa)
                                                          .ExecuteUpdateAsync(setters => setters
                                                                     .SetProperty(cp => cp.IdPessoa, contatoPessoaEntity.IdPessoa)
                                                                     .SetProperty(cp => cp.IdRegiao, contatoPessoaEntity.IdRegiao)
                                                                     .SetProperty(cp => cp.Numero, contatoPessoaEntity.Numero)
                                                                     .SetProperty(cp => cp.TipoContatoPessoa, contatoPessoaEntity.TipoContatoPessoa));


                return (result != 0);
            }
            catch (Exception ex) 
            {
                logger?.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> ExcluirContatoPessoa(int idContatoPessoa)
        {
            try 
            {
                var contatoPessoa = context.ContatosPessoas
                                       .Where(cp => cp.Id == idContatoPessoa)
                                       .AsNoTracking()
                                       .FirstOrDefault();

                var result = (contatoPessoa is not null)?await context.ContatosPessoas.Remove(contatoPessoa).Context.SaveChangesAsync() : 0;

                return (result != 0);
            }
            catch (Exception ex) 
            {
                logger?.LogError(ex.Message);
                return false;
            }
        }

        public ContatoPessoaEntity? GetContatoPessoa(int idContatoPessoa)
        {
            try 
            {
                var contatoPessoa = context.ContatosPessoas
                                    .Where(cp => cp.Id == idContatoPessoa)
                                    .Include(p => p.Pessoa)
                                    .Include(r => r.Regiao)
                                    .AsNoTracking()
                                    .FirstOrDefault();

                return (contatoPessoa is not null)? contatoPessoa : null;
            }
            catch (Exception ex) 
            {
                logger?.LogError(ex.Message);
                return null;
            }
             
        }

        public List<ContatoPessoaEntity>? GetContatosPessoas()
        {
            try 
            { 
                var contatosPessoas = context.ContatosPessoas
                                     .Select(cp => cp)
                                     .AsNoTracking()
                                     .ToList();

                return contatosPessoas;
            }
            catch (Exception ex) 
            {
                logger?.LogError(ex.Message);
                return new List<ContatoPessoaEntity>();
            }

             
        }

    }
}
