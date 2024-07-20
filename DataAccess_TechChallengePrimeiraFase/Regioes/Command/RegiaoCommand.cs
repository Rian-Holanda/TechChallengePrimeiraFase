using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess_TechChallengePrimeiraFase.Regioes.Interface;
using Entities_TechChallengePrimeiraFase.Entities;
using Infrastructure_TechChallengePrimeiraFase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess_TechChallengePrimeiraFase.Regioes.Command
{
    public class RegiaoCommand : IRegiaoCommand
    {
        private readonly IApplicationDbContext context;
        private readonly ILogger<RegiaoCommand>? logger;

        public RegiaoCommand( IApplicationDbContext context, ILogger<RegiaoCommand>? logger)
        {
            this.context = context;
            this.logger = logger;
        }


        public int InserirRegiao(RegioesEntity regioesEntity)
        {
            try 
            {
                return context.Regioes.Add(regioesEntity).Context.SaveChanges();
            }
            catch (Exception ex) 
            {
                logger?.LogError(ex.Message);
                return 0;
            }
        }

        public bool AlterarRegiao(RegioesEntity regioesEntity, int idRegiao)
        {
            try 
            {
                var regiao = context.Regioes.Where(r => r.Id == idRegiao).FirstOrDefault();
                regiao = regioesEntity;

                var result =  context.Regioes.Update(regioesEntity).Context.SaveChanges();

                return (result != 0);
            }
            catch (Exception ex) 
            {
                logger?.LogError(ex.Message);
                return false;
            }
        }

        public bool ExcluirRegiao (int idRegiao) 
        {
            try 
            {
                var regiao = context.Regioes.Where(r => r.Id == idRegiao).FirstOrDefault();
                var result = (regiao is not null )?context.Regioes.Remove(regiao).Context.SaveChanges(): 0;

                return (result != 0);
            }
            catch (Exception ex) 
            {
                logger?.LogError(ex.Message);
                return false;
            }
        }

        public RegioesEntity? GetRegiao(int idRegiao) 
        {
            try 
            {
                var regiao = context.Regioes.Where(r => r.Id == idRegiao).FirstOrDefault();

                return (regiao is not null)? regiao: null;
            }
            catch (Exception ex) 
            {
                logger?.LogError(ex.Message);
                return null;
            }
             
        }

        public List<RegioesEntity> GetRegioes()
        {
            try 
            { 
                var regioes = context.Regioes
                                     .Select(r => r)
                                     .Include(cr => cr.RegiaoCodigoAreas)
                                     .ToList();

                return regioes;
            }
            catch (Exception ex) 
            {
                logger?.LogError(ex.Message);
                return new List<RegioesEntity>();
            }

             
        }

    }
}
