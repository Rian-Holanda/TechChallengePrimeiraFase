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

        public int InserirPessoa(PessoaEntity pessoaEntity)
        {
            try 
            {
                return context.Pessoas.Add(pessoaEntity).Context.SaveChanges();
            }
            catch (Exception ex) 
            {
                logger?.LogError(ex.Message);
                return 0;
            }
        }

        public bool AlterarPessoa(PessoaEntity pessoaEntity)
        {
            try 
            {
                var result = context.Pessoas.Update(pessoaEntity).Context.SaveChanges();

                return (result != 0);
            }
            catch (Exception ex) 
            {
                logger?.LogError(ex.Message);
                return false;
            }
        }

        public bool ExcluirPessoa(int idPessoa)
        {
            try 
            {
                var pessoa = context.Pessoas
                                       .Where(p => p.Id == idPessoa)
                                       .FirstOrDefault();

                var result = (pessoa is not null)?context.Pessoas.Remove(pessoa).Context.SaveChanges(): 0;

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
                var pessoa = context.Pessoas.Where(cp => cp.Id == idPessoa).FirstOrDefault();

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
