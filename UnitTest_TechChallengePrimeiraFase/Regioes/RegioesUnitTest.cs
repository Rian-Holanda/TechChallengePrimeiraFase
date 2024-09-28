using Business_TechChallengePrimeiraFase.Regioes.Application.Interfaces;
using Business_TechChallengePrimeiraFase.Regioes.Domain;
using Castle.Core.Logging;
using DataAccess_TechChallengePrimeiraFase.Regioes.Command;
using DataAccess_TechChallengePrimeiraFase.Regioes.Queries;
using Entities_TechChallengePrimeiraFase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
    public class RegioesUnitTest
    {
        ApplicationDbContext _context;

        private Mock<IValidacoesRegioes> _mockValidacoesRigioes = new Mock<IValidacoesRegioes>();
        private ILogger<RegiaoQueries>? _logger;
        private ILogger<RegiaoCommand>? loggerRegiaoCommand;


        //using the same connection string
        public static string connectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBFACULDADE;Integrated Security=True;";

        public RegioesUnitTest()
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
        public void ValidaRegioes()
        {
            if (_logger is not null)
            {
                string? sigla = "SP";

                _mockValidacoesRigioes.Setup(m => m.ValidaRegiaoExistente("SP")).Returns(true);
                RegiaoQueries regiaoQueries = new RegiaoQueries(_context, _logger);
                // act
                var resultadoEsperado = _mockValidacoesRigioes.Object.ValidaRegiaoExistente("SP");
                var resultado = regiaoQueries.GetRegiaoExistente("SP");


                // assert
                Assert.Equal(resultado.Sigla, sigla);
            }

        }

        [Fact]
        public async void ValidaInsertRegiao()
        {
            if (loggerRegiaoCommand is not null)
            {
                RegiaoEntity regioesEntity = new RegiaoEntity
                {
                    Sigla = "RJ"
                };

                RegiaoCommand regiaoCommand = new RegiaoCommand(_context, loggerRegiaoCommand);

                int resultado = await  regiaoCommand.InserirRegiao(regioesEntity);

                Assert.True(resultado > 0);
            }

        }

        [Fact]
        public void ValidaGetRegioes()
        {
            if (loggerRegiaoCommand is not null)
            {
                RegiaoCommand regiaoCommand = new RegiaoCommand(_context, loggerRegiaoCommand);

                Assert.True(regiaoCommand.GetRegioes().Count > 0);
            }
        }

        [Fact]
        public void ValidaGetRegiao()
        {
            if (loggerRegiaoCommand is not null)
            {

                RegiaoCommand regiaoCommand = new RegiaoCommand(_context, loggerRegiaoCommand);

                var regioes = regiaoCommand.GetRegioes();
                var regiao = regioes.Last();

                Assert.True(regiaoCommand.GetRegiao(regiao.Id) != null);
            }

        }

        [Fact]

        public async void UpdateRegiao()
        {
            if (loggerRegiaoCommand is not null)
            {
                RegiaoCommand regiaoCommand = new RegiaoCommand(_context, loggerRegiaoCommand);

                var regioes = regiaoCommand.GetRegioes();
                var regiao = regiaoCommand.GetRegiao(2);

                if (regiao is not null)
                {
                    regiao.Sigla = "MG";

                    bool result = await regiaoCommand.AlterarRegiao(regiao);

                    Assert.True(result);
                }

            }


        }

        [Fact]
        public async void DeleteRegiao()
        {
            if (loggerRegiaoCommand is not null)
            {
                RegiaoCommand regiaoCommand = new RegiaoCommand(_context, loggerRegiaoCommand);

                var regioes = regiaoCommand.GetRegioes();
                var regiao = regiaoCommand.GetRegiao(1002);

                if (regiao is not null)
                {
                    Assert.True(await regiaoCommand.ExcluirRegiao(regiao.Id));
                }
            }


        }




    }
}
