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
    public class RegiaoCodigoAreaCommand : IRegiaoCodigoAreaCommand
    {
        private readonly IApplicationDbContext context;
        private readonly ILogger<RegiaoCodigoAreaCommand> logger;

        public RegiaoCodigoAreaCommand( IApplicationDbContext context, ILogger<RegiaoCodigoAreaCommand> logger)
        {
            this.context = context;
            this.logger = logger;
        }


        public int InserirRegiaoCodigoArea(RegioesCodigosAreasEntity regioesCodigosAreasEntity)
        {
            try 
            {
                return context.RegioesCodigosAreas.Add(regioesCodigosAreasEntity).Context.SaveChanges();
            }
            catch (Exception ex) 
            {
                return 0;
            }
        }

        public bool AlterarRegiaoCodigoArea(RegioesCodigosAreasEntity regioesCodigosAreasEntity, int idRegiaoCodigoArea)
        {
            try 
            {
                var regiaoCodigoArea = context.RegioesCodigosAreas.Where(r => r.Id == idRegiaoCodigoArea).FirstOrDefault();
                regiaoCodigoArea = regioesCodigosAreasEntity;

                var result = context.RegioesCodigosAreas.Update(regiaoCodigoArea).Context.SaveChanges();

                return (result != 0);
            }
            catch (Exception ex) 
            { 
                return false;
            }
        }

        public bool ExcluirRegiaoCodigoArea(int idRegiaoCodigoArea)
        {
            try 
            {
                var regiaoCodigoArea = context.RegioesCodigosAreas
                                       .Where(r => r.Id == idRegiaoCodigoArea)
                                       .FirstOrDefault();

                var result = (regiaoCodigoArea is not null)?context.RegioesCodigosAreas.Remove(regiaoCodigoArea).Context.SaveChanges(): 0;

                return (result != 0);
            }
            catch (Exception ex) 
            {
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
                return null;
            }
             
        }

        public List<RegioesCodigosAreasEntity>? GetRegioesCodigosAreas()
        {
            try 
            { 
                var regioesCodigosAreas = context.RegioesCodigosAreas.Select(r => r).ToList();

                return regioesCodigosAreas;
            }
            catch (Exception ex) 
            {
                return new List<RegioesCodigosAreasEntity>();
            }

             
        }

    }
}
