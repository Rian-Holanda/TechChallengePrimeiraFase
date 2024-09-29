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
    public class RegiaoCodigoAreaCommand : IRegiaoCodigoAreaCommand
    {
        private readonly IApplicationDbContext context;
        private readonly ILogger<RegiaoCodigoAreaCommand> logger;

        public RegiaoCodigoAreaCommand( IApplicationDbContext context, ILogger<RegiaoCodigoAreaCommand> logger)
        {
            this.context = context;
            this.logger = logger;
        }


        public async Task<int> InserirRegiaoCodigoArea(RegiaoCodigoAreaEntity regioesCodigosAreasEntity)
        {
            try 
            {

                return await context.RegioesCodigosAreas.Add(regioesCodigosAreasEntity).Context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                logger.LogError(ex.Message);
                return 0;
            }
        }

        public async Task<bool> AlterarRegiaoCodigoArea(RegiaoCodigoAreaEntity regioesCodigosAreasEntity, int id)
        {
            try 
            {
                var result = await context.RegioesCodigosAreas.Where(r => r.Id == id)
                                                              .ExecuteUpdateAsync(setters => setters
                                                                                 .SetProperty(rc => rc.DDD, regioesCodigosAreasEntity.DDD)
                                                                                 .SetProperty(rc => rc.IdRegiao, regioesCodigosAreasEntity.IdRegiao));

                
                return (result != 0);
            }
            catch (Exception ex) 
            {
                logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> ExcluirRegiaoCodigoArea(int idRegiaoCodigoArea)
        {
            try 
            {
                var regiaoCodigoArea = context.RegioesCodigosAreas
                                       .Where(r => r.Id == idRegiaoCodigoArea)
                                       .AsNoTracking()
                                       .FirstOrDefault();

                var result = (regiaoCodigoArea is not null)?await context.RegioesCodigosAreas.Remove(regiaoCodigoArea).Context.SaveChangesAsync() : 0;

                return (result != 0);
            }
            catch (Exception ex) 
            {
                logger.LogError(ex.Message);
                return false;
            }
        }

        public RegiaoCodigoAreaEntity? GetRegiaoCodigoArea(int idRegiaoCodigoArea)
        {
            try 
            {
                var regiaoCodigoArea = context.RegioesCodigosAreas
                    .Where(r => r.Id == idRegiaoCodigoArea)
                    .AsNoTracking()
                    .FirstOrDefault();

                return (regiaoCodigoArea is not null)? regiaoCodigoArea : null;
            }
            catch (Exception ex) 
            {
                logger.LogError(ex.Message);
                return null;
            }
             
        }

        public List<RegiaoCodigoAreaEntity>? GetRegioesCodigosAreas()
        {
            try 
            { 
                var regioesCodigosAreas = context.RegioesCodigosAreas
                                                 .Select(r => r)
                                                 .Include(r => r.Regiao)
                                                 .AsNoTracking()
                                                 .ToList();

                return regioesCodigosAreas;
            }
            catch (Exception ex) 
            {
                logger.LogError(ex.Message);
                return new List<RegiaoCodigoAreaEntity>();
            }

             
        }

    }
}
