using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess_TechChallengePrimeiraFase.Contatos.Interface;
using Entities_TechChallengePrimeiraFase.Entities;
using Infrastructure_TechChallengePrimeiraFase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess_TechChallengePrimeiraFase.Contatos.Command
{
    public class PessoasCommand : IPessoasCommand
    {
        private readonly IApplicationDbContext context;
        private readonly ILogger<PessoasCommand>? logger;

        public PessoasCommand( IApplicationDbContext context, ILogger<PessoasCommand>? logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<int> InserirPessoa(PessoaEntity pessoaEntity)
        {
            try 
            {
                return await context.Pessoas.Add(pessoaEntity).Context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                logger?.LogError(ex.Message);
                return 0;
            }
        }

        public async Task<bool> AlterarPessoa(PessoaEntity pessoaEntity, int id)
        {
            try 
            {
                var result = await context.Pessoas.Where(p => p.Id == id)
                                                 .ExecuteUpdateAsync(setters => setters
                                                                    .SetProperty(p => p.Nome, pessoaEntity.Nome)
                                                                    .SetProperty(p => p.Email, pessoaEntity.Email));


                return (result != 0);
            }
            catch (Exception ex) 
            {
                logger?.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> ExcluirPessoa(int idPessoa)
        {
            try 
            {
                var result = (idPessoa > 0)? await  context.Pessoas.Where(p => p.Id == idPessoa).ExecuteDeleteAsync():0;

                return (result != 0);
            }
            catch (Exception ex) 
            {
                logger?.LogError(ex.Message);
                return false;
            }
        }

        public PessoaEntity? GetPessoa(int idPessoa)
        {
            try 
            {
                var pessoa = context.Pessoas
                    .Where(cp => cp.Id == idPessoa)
                    .AsNoTracking()
                    .FirstOrDefault();

                return (pessoa is not null)? pessoa : null;
            }
            catch (Exception ex) 
            {
                logger?.LogError(ex.Message);
                return null;
            }
             
        }

        public List<PessoaEntity>? GetPessoas()
        {
            try 
            { 
                var pessoas = context.Pessoas
                                     .Select(p => p)
                                     .Include(cp => cp.ContatoPessoa)
                                     .AsNoTracking()
                                     .ToList();

                return pessoas;
            }
            catch (Exception ex) 
            {
                logger?.LogError(ex.Message);
                return new List<PessoaEntity>();
            }

             
        }

    }
}
