using Business_TechChallengePrimeiraFase.Contatos.Applicacation.Interfaces;
using Business_TechChallengePrimeiraFase.Regioes.Application.Interfaces;
using Business_TechChallengePrimeiraFase.Regioes.Domain;
using Castle.Core.Logging;
using DataAccess_TechChallengePrimeiraFase.Regioes.Queries;
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
        private ILogger<RegiaoQueries> _logger;

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
}
