using DataAccess_TechChallengePrimeiraFase.Regioes.Command;
using Entities_TechChallengePrimeiraFase.Entities;
using Infrastructure_TechChallengePrimeiraFase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Reflection.Metadata;

namespace UnitTest_TechChallengePrimeiraFase
{
    public class UnitTest1
    {
        ApplicationDbContext _context;
   
        //using the same connection string
        public static string connectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBFACULDADE;Integrated Security=True;";

        public UnitTest1()
        {
            var serviceProvider = new ServiceCollection()
                                      .AddEntityFrameworkSqlServer()
                                      .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(connectionString)
                   .UseInternalServiceProvider(serviceProvider);

            _context = new ApplicationDbContext(builder.Options);


        }

    }
}