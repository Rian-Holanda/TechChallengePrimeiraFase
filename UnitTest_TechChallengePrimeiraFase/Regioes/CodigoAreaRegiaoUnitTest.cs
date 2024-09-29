using Business_TechChallengePrimeiraFase.Regioes.Application.Interfaces;
using DataAccess_TechChallengePrimeiraFase.Regioes.Command;
using DataAccess_TechChallengePrimeiraFase.Regioes.Queries;
using Entities_TechChallengePrimeiraFase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest_TechChallengePrimeiraFase.Regioes
{
    public class CodigoAreaRegiaoUnitTest
    {
        ApplicationDbContext _context;

        private ILogger<RegiaoCodigoAreaQueries>? _logger;
        private ILogger<RegiaoCodigoAreaCommand>? loggerRegiaoCodigoAreaCommand;
        private ILogger<RegiaoCommand>? loggerRegiaoCommand;
        private ILogger<RegiaoCodigoAreaQueries>? loggerRegiaoCodigoAreaQueries;

        //using the same connection string
        public static string connectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBFACULDADE;Integrated Security=True;";

        public CodigoAreaRegiaoUnitTest()
        {
            var serviceProvider = new ServiceCollection()
                            .AddEntityFrameworkSqlServer()
                            .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            builder.UseSqlServer(connectionString)
                   .UseInternalServiceProvider(serviceProvider);


            _context = new ApplicationDbContext(builder.Options);


        }

        [Fact]
        public async void ValidaInsertRegiaoCodigoArea()
        {
            if (loggerRegiaoCommand is not null)
            {
                RegiaoCommand regiaoCommand = new RegiaoCommand(_context, loggerRegiaoCommand);

                var regiao = regiaoCommand.GetRegiao(2);

                if (regiao is not null)
                {
                    RegiaoCodigoAreaEntity regioesCodigosAreasEntity = new RegiaoCodigoAreaEntity()
                    {
                        
                        DDD = 24,
                        IdRegiao = regiao.Id

                    };

                    if (loggerRegiaoCodigoAreaCommand is not null) 
                    {
                        RegiaoCodigoAreaCommand RegiaoCodigoAreaCommand = new RegiaoCodigoAreaCommand(_context, loggerRegiaoCodigoAreaCommand);

                        Assert.True(await RegiaoCodigoAreaCommand.InserirRegiaoCodigoArea(regioesCodigosAreasEntity) > 0);
                    }

                     
                }
            }


        }

        [Fact]
        public void ValidaGetRegioesCodigoArea()
        {
            if (loggerRegiaoCodigoAreaCommand is not null)
            {
                RegiaoCodigoAreaCommand RegiaoCodigoAreaCommand = new RegiaoCodigoAreaCommand(_context, loggerRegiaoCodigoAreaCommand);

                Assert.True(RegiaoCodigoAreaCommand.GetRegioesCodigosAreas()?.Count > 0);
            }

        }

        [Fact]
        public void ValidaGetRegiaoCodigoArea()
        {
            if (loggerRegiaoCodigoAreaCommand is not null) 
            {

                RegiaoCodigoAreaCommand RegiaoCodigoAreaCommand = new RegiaoCodigoAreaCommand(_context, loggerRegiaoCodigoAreaCommand);

                var regioesCodigoArea = RegiaoCodigoAreaCommand.GetRegioesCodigosAreas();
                var regiaoCodigoArea = regioesCodigoArea?.Last();

                if (regiaoCodigoArea is not null)
                {
                    Assert.True(RegiaoCodigoAreaCommand.GetRegiaoCodigoArea(regiaoCodigoArea.Id) != null);
                }
            }
        }

        [Fact]

        public async void UpdateRegiaoCodigoArea()
        {
            if (loggerRegiaoCodigoAreaCommand is not null) 
            {
                RegiaoCodigoAreaCommand RegiaoCodigoAreaCommand = new RegiaoCodigoAreaCommand(_context, loggerRegiaoCodigoAreaCommand);

                var regiaoCodigoArea = RegiaoCodigoAreaCommand.GetRegioesCodigosAreas()?.LastOrDefault();
                
                if (regiaoCodigoArea is not null)
                {
                    regiaoCodigoArea.DDD = 33;

                    Assert.True(await RegiaoCodigoAreaCommand.AlterarRegiaoCodigoArea(regiaoCodigoArea,regiaoCodigoArea.Id));
                }
            }
        }

        [Fact]
        public async void DeleteRegiaoCodigoArea()
        {
            if (loggerRegiaoCodigoAreaCommand is not null) 
            {
                RegiaoCodigoAreaCommand RegiaoCodigoAreaCommand = new RegiaoCodigoAreaCommand(_context, loggerRegiaoCodigoAreaCommand);

                var regiaoCodigoArea = RegiaoCodigoAreaCommand.GetRegioesCodigosAreas()?.LastOrDefault();
                
                if (regiaoCodigoArea is not null)
                {
                    Assert.True(await RegiaoCodigoAreaCommand.ExcluirRegiaoCodigoArea(regiaoCodigoArea.Id));
                }
            }
    


        }

        [Fact]
        public void ValidaQueries()
        {
            if (loggerRegiaoCodigoAreaQueries is not null) 
            {
                RegiaoCodigoAreaQueries regiaoCodigoAreaQueries = new RegiaoCodigoAreaQueries(_context, loggerRegiaoCodigoAreaQueries);

                var sigla = regiaoCodigoAreaQueries.GetSiglaCodigoArea(32);
                var DDDs = regiaoCodigoAreaQueries.GetDDDs("MG");

                Assert.True(sigla != "" && DDDs.Count() > 0);
            }
        }

    }
}
