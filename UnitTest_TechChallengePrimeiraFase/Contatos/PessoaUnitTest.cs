﻿using DataAccess_TechChallengePrimeiraFase.Contatos.Command;
using DataAccess_TechChallengePrimeiraFase.Contatos.Queries;
using DataAccess_TechChallengePrimeiraFase.Regioes.Command;
using DataAccess_TechChallengePrimeiraFase.Regioes.Queries;
using Entities_TechChallengePrimeiraFase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest_TechChallengePrimeiraFase.Contatos
{
    public class PessoaUnitTest
    {
        ApplicationDbContext _context;
        private ILogger<PessoasQueries>? _logger;
        private ILogger<PessoasCommand>? loggerPessoaCommand;


        //using the same connection string
        public static string connectionString = $"Data Source=docker-desktop,30003;Initial Catalog=FIAP_TECH_CHALLENGE;Persist Security Info=True;User ID=sa;Password=1q2w3e4r@#;Encrypt=True;Trust Server Certificate=True;";

        public PessoaUnitTest()
        {
            var serviceProvider = new ServiceCollection()
                            .AddEntityFrameworkSqlServer()
                            .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            builder.UseSqlServer(connectionString)
                   .UseInternalServiceProvider(serviceProvider);


            _context = new ApplicationDbContext(builder.Options);

        }

        //[Fact]
        //public void ValidaInsertPessoa()
        //{
        //    PessoasEntity pessoasEntity = new PessoasEntity()
        //    {
        //        Nome = "Rian",
        //        Email = "Rian.Holanda@teste.com"

        //    };

        //    PessoasCommand pessoasCommand = new PessoasCommand(_context, loggerPessoaCommand);

        //    var result = pessoasCommand.InserirPessoa(pessoasEntity);

        //    Assert.True(result > 0);

        //}

        //[Fact]
        //public void ValidaGetPessoa()
        //{

        //    PessoasCommand pessoasCommand = new PessoasCommand(_context, loggerPessoaCommand);


        //    var pessoas = pessoasCommand.GetPessoas();

        //    if (pessoas != null)
        //    {
        //        if (pessoas.Count > 0)
        //        {
        //            Assert.True(pessoasCommand.GetPessoa(pessoas.First().Id) != null);
        //        }
        //    }

        //}

        //[Fact]
        //public void ValidaGetPessoas()
        //{

        //    PessoasCommand pessoasCommand = new PessoasCommand(_context, loggerPessoaCommand);

        //    Assert.True(pessoasCommand.GetPessoas()?.Count() > 0);

        //}

        //[Fact]
        //public void ValidaAlterarPessoa()
        //{

        //    PessoasCommand pessoasCommand = new PessoasCommand(_context, loggerPessoaCommand);

        //    var pessoa = pessoasCommand.GetPessoa(2);

        //    if (pessoa is not null)
        //    {
        //        pessoa.Email = "rian.holanda@teste.com.br";

        //        Assert.True(pessoasCommand.AlterarPessoa(pessoa, pessoa.Id));
        //    }
        //}

        //[Fact]
        //public void ValidaDeletarPessoa()
        //{

        //    PessoasCommand pessoasCommand = new PessoasCommand(_context, loggerPessoaCommand);

        //    var pessoas = pessoasCommand.GetPessoas();

        //    if (pessoas != null)
        //    {
        //        if (pessoas.Count > 0)
        //        {
        //            var pessoa = pessoasCommand.GetPessoa(pessoas.First().Id);

        //            if (pessoa is not null)
        //            {
        //                Assert.True(pessoasCommand.ExcluirPessoa(pessoa.Id));
        //            }
        //        }
        //    }
        //}
    }
}
