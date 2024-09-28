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


        public async Task<int> InserirRegiaoCodigoArea(RegioesCodigosAreasEntity regioesCodigosAreasEntity)
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

        public async Task<bool> AlterarRegiaoCodigoArea(RegioesCodigosAreasEntity regioesCodigosAreasEntity, int idRegiaoCodigoArea)
        {
            try 
            {
                var regiaoCodigoArea = context.RegioesCodigosAreas.Where(r => r.Id == idRegiaoCodigoArea).FirstOrDefault();
                regiaoCodigoArea = regioesCodigosAreasEntity;

                var result = await context.RegioesCodigosAreas.Update(regiaoCodigoArea).Context.SaveChangesAsync();

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

        public RegioesCodigosAreasEntity? GetRegiaoCodigoArea(int idRegiaoCodigoArea)
        {
            try 
            {
                var regiaoCodigoArea = context.RegioesCodigosAreas.Where(r => r.Id == idRegiaoCodigoArea).FirstOrDefault();

                return (regiaoCodigoArea is not null)? regiaoCodigoArea : null;
            }
            catch (Exception ex) 
            {
                logger.LogError(ex.Message);
                return null;
            }
             
        }

        public List<RegioesCodigosAreasEntity>? GetRegioesCodigosAreas()
        {
            try 
            { 
                var regioesCodigosAreas = context.RegioesCodigosAreas
                                                 .Select(r => r)
                                                 .Include(r => r.Regiao)
                                                 .ToList();

                return regioesCodigosAreas;
            }
            catch (Exception ex) 
            {
                logger.LogError(ex.Message);
                return new List<RegioesCodigosAreasEntity>();
            }

             
        }

    }
}
