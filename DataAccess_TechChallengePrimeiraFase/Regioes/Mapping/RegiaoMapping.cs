using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities_TechChallengePrimeiraFase.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess_TechChallengePrimeiraFase.Regioes.Mapping
{
    public class RegiaoMapping
    {
        public readonly MapperConfiguration configurationAreaMapping = new MapperConfiguration(config =>
        {
            config.CreateMap<DbSet<Entities_TechChallengePrimeiraFase.Entities.Regioes>, Entities_TechChallengePrimeiraFase.Entities.Regioes>();
            config.CreateMap<Entities_TechChallengePrimeiraFase.Entities.Regioes, DbSet<Entities_TechChallengePrimeiraFase.Entities.Regioes>>();
        });

        //public AreaModel ConvertAreaEntityMapper(AreaEntity areaEntity)
        //{
        //    AreaModel areaModel = new AreaModel();
        //    try
        //    {
        //        var mapper = configurationAreaMapping.CreateMapper();

        //        var areaEntityMapper = mapper.Map<AreaModel>(areaEntity);

        //        areaModel = areaEntityMapper;

        //        return areaModel;
        //    }
        //    catch (Exception)
        //    {
        //        return areaModel;
        //    }

        //}


        public Entities_TechChallengePrimeiraFase.Entities.Regioes EntityMapper(DbSet<Entities_TechChallengePrimeiraFase.Entities.Regioes> regiaoContext)
        {
            Entities_TechChallengePrimeiraFase.Entities.Regioes regiaoEntity = new Entities_TechChallengePrimeiraFase.Entities.Regioes();
            try
            {
                var mapper = configurationAreaMapping.CreateMapper();

                var regiaoEntityMapper = mapper.Map<Entities_TechChallengePrimeiraFase.Entities.Regioes>(regiaoContext);

                regiaoEntity = regiaoEntityMapper;

                return regiaoEntity;
            }
            catch (Exception)
            {
                return regiaoEntity;
            }

        }


        //public List<AreaEntity> ConvertListAreaModelMapper(IQueryable<AreaModel> listAreasModel)
        //{
        //    List<AreaEntity> listAreas = new List<AreaEntity>();
        //    try
        //    {

        //        var mapper = configurationAreaMapping.CreateMapper();

        //        var AreaEntityMapper = mapper.Map<List<AreaEntity>>(listAreasModel);

        //        listAreas = AreaEntityMapper;

        //        return listAreas;
        //    }
        //    catch (Exception)
        //    {
        //        return listAreas;
        //    }

        //}

        //public List<AreaModel> ConvertListAreaEntityMapper(List<AreaEntity> listAreasEntity)
        //{
        //    List<AreaModel> listAreas = new List<AreaModel>();
        //    try
        //    {

        //        var mapper = configurationAreaMapping.CreateMapper();

        //        var AreaEntityMapper = mapper.Map<List<AreaModel>>(listAreasEntity);

        //        listAreas = AreaEntityMapper;

        //        return listAreas;
        //    }
        //    catch (Exception)
        //    {
        //        return listAreas;
        //    }

        //}
    }
}
