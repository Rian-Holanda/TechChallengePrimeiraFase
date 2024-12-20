﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess_TechChallengePrimeiraFase.Regioes.Interface;
using Entities_TechChallengePrimeiraFase.Entities;
using Infrastructure_TechChallengePrimeiraFase;
using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Gateway;
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


        public async Task<int> InserirRegiao(RegiaoEntity regioesEntity)
        {
            try 
            {
                return await context.Regioes.Add(regioesEntity).Context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                logger?.LogError(ex.Message);
                return 0;
            }
        }

        public async Task<bool> AlterarRegiao(RegiaoEntity regioesEntity, int id)
        {
            try 
            {
                var regiao = GetRegiao(id);
                regiao = new RegiaoEntity()
                {
                    Id = id,
                    Sigla = regioesEntity.Sigla,
                };


                var result = await context.Regioes.Where(r => r.Id == id)
                                                  .ExecuteUpdateAsync(setters => setters
                                                                     .SetProperty(r => r.Sigla, regioesEntity.Sigla));

                return (result != 0);
            }
            catch (Exception ex) 
            {
                logger?.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> ExcluirRegiao (int idRegiao) 
        {
            try 
            {
                var result = (idRegiao > 0) ? await context.Regioes.Where(p => p.Id == idRegiao).ExecuteDeleteAsync() : 0;

                return (result != 0);
            }
            catch (Exception ex) 
            {
                logger?.LogError(ex.Message);
                return false;
            }
        }

        public RegiaoEntity? GetRegiao(int idRegiao) 
        {
            try 
            {
                var regiao = context.Regioes
                    .Where(r => r.Id == idRegiao)
                    .AsNoTracking()
                    .FirstOrDefault();

                return (regiao is not null)? regiao: null;
            }
            catch (Exception ex) 
            {
                logger?.LogError(ex.Message);
                return null;
            }
             
        }

        public List<RegiaoEntity> GetRegioes()
        {
            try 
            { 
                var regioes = context.Regioes
                                     .Select(r => r)
                                     .Include(cr => cr.RegiaoCodigoAreas)
                                     .AsNoTracking()
                                     .ToList();

                return regioes;
            }
            catch (Exception ex) 
            {
                logger?.LogError(ex.Message);
                return new List<RegiaoEntity>();
            }

             
        }

    }
}
