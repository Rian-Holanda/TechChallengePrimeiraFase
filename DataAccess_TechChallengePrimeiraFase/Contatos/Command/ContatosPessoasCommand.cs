using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess_TechChallengePrimeiraFase.Regioes.Interface;
using Entities_TechChallengePrimeiraFase.Entities;
using Infrastructure_TechChallengePrimeiraFase;
using Microsoft.Extensions.Logging;

namespace DataAccess_TechChallengePrimeiraFase.Regioes.Command
{
    public class ContatosPessoasCommand : IContatosPessoasCommand
    {
        private readonly IApplicationDbContext context;
        private readonly ILogger<ContatosPessoasCommand> logger;

        public ContatosPessoasCommand( IApplicationDbContext context, ILogger<ContatosPessoasCommand> logger)
        {
            this.context = context;
            this.logger = logger;
        }


        public int InserirContatoPessoa(ContatosPessoaEntity contatoPessoaEntity)
        {
            try 
            {
                return context.ContatosPessoas.Add(contatoPessoaEntity).Context.SaveChanges();
            }
            catch (Exception ex) 
            {
                return 0;
            }
        }

        public bool AlterarContatoPessoa(ContatosPessoaEntity contatoPessoaEntity, int idContatoPessoa)
        {
            try 
            {
                var contatoPessoa = context.ContatosPessoas.Where(cp => cp.Id == idContatoPessoa).FirstOrDefault();
                contatoPessoa = contatoPessoaEntity;

                var result = context.ContatosPessoas.Update(contatoPessoa).Context.SaveChanges();

                return (result != 0);
            }
            catch (Exception ex) 
            { 
                return false;
            }
        }

        public bool ExcluirContatoPessoa(int idContatoPessoa)
        {
            try 
            {
                var contatoPessoa = context.ContatosPessoas
                                       .Where(cp => cp.Id == idContatoPessoa)
                                       .FirstOrDefault();

                var result = (contatoPessoa is not null)?context.ContatosPessoas.Remove(contatoPessoa).Context.SaveChanges(): 0;

                return (result != 0);
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

        public ContatosPessoaEntity? GetContatoPessoa(int idContatoPessoa)
        {
            try 
            {
                var contatoPessoa = context.ContatosPessoas.Where(cp => cp.Id == idContatoPessoa).FirstOrDefault();

                return (contatoPessoa is not null)? contatoPessoa : null;
            }
            catch (Exception ex) 
            {
                return null;
            }
             
        }

        public List<ContatosPessoaEntity>? GetContatosPessoas()
        {
            try 
            { 
                var contatosPessoas = context.ContatosPessoas.Select(cp => cp).ToList();

                return contatosPessoas;
            }
            catch (Exception ex) 
            {
                return new List<ContatosPessoaEntity>();
            }

             
        }

    }
}
