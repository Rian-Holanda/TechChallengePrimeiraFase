using Business_TechChallengePrimeiraFase.Contatos.Domain;
using DataAccess_TechChallengePrimeiraFase.Contatos.Queries;
using DataAccess_TechChallengePrimeiraFase.Regioes.Command;
using Entities_TechChallengePrimeiraFase.Entities;
using Entities_TechChallengePrimeiraFase.Enuns;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business_TechChallengePrimeiraFase.Contatos.Domain.ValidaContatoPessoa;

namespace UnitTest_TechChallengePrimeiraFase.Contatos
{
    public class PessoaContatoUnitTest
    {
        ApplicationDbContext _context;
        private ILogger<PessoasQueries>? _logger;
        private ILogger<PessoasCommand>? loggerPessoaCommand;
        private ILogger<ContatosPessoasCommand>? loggerContatoPessoaCommand;
        private ILogger<RegiaoCommand>? loggerRegiaoCommand; 


        //using the same connection string
        public static string connectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBFACULDADE;Integrated Security=True;";

        public PessoaContatoUnitTest()
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
        public void ValidaInsertContatoPessoa() 
        {
            RegiaoCommand regiaoCommand = new RegiaoCommand(_context, loggerRegiaoCommand);
            PessoasCommand pessoasCommand = new PessoasCommand(_context, loggerPessoaCommand);
            ContatosPessoasCommand contatosPessoasCommand = new ContatosPessoasCommand(_context, loggerContatoPessoaCommand);

            var pessoa = pessoasCommand.GetPessoa(2);
            var regiao = regiaoCommand.GetRegiao(2);
            if(pessoa is not null && regiao is not null) 
            {
                ContatoPessoaDomain contatoPessoaDomain = new ContatoPessoaDomain();

                string contato1 = "3256780987";
                string contato2 = "32956780987";

                ContatosPessoaEntity contatosPessoaEntity1 = new ContatosPessoaEntity()
                {
                    Pessoa = pessoa,
                    Regiao = regiao,
                    Numero = contato1,
                    IdPessoa = pessoa.Id,
                    IdRegiao = regiao.Id,
                    TipoContatoPessoa = (int)TipoContatoEnum.Fixo
                };

                var tipoContatoFixo = contatoPessoaDomain.ValidaTipoContato(new ValidaContatoPessoaFixo(), contatosPessoaEntity1);

                ContatosPessoaEntity contatosPessoaEntity2 = new ContatosPessoaEntity()
                {
                    Pessoa = pessoa,
                    Regiao = regiao,
                    Numero = contato2,
                    IdPessoa = pessoa.Id,
                    IdRegiao = regiao.Id,
                    TipoContatoPessoa = (int)TipoContatoEnum.Celular
                };

                var tipoContatoCelular = contatoPessoaDomain.ValidaTipoContato(new ValidaContatoPessoaCelular(), contatosPessoaEntity2);

                if (tipoContatoFixo && tipoContatoCelular) 
                {
                    var result1 = contatosPessoasCommand.InserirContatoPessoa(contatosPessoaEntity1);
                    var result2 = contatosPessoasCommand.InserirContatoPessoa(contatosPessoaEntity2);

                    Assert.True(result1 > 0 && result2 > 0);
                }

            }
        }

        [Fact]
        public void ValidaGetContatoPessoa()
        {
            ContatosPessoasCommand contatosPessoasCommand = new ContatosPessoasCommand(_context, loggerContatoPessoaCommand);

            var contatoPessoa = contatosPessoasCommand.GetContatoPessoa(1);

            Assert.NotNull(contatoPessoa);
        }

        [Fact]
        public void ValidaGetContatosPessoas()
        {
            ContatosPessoasCommand contatosPessoasCommand = new ContatosPessoasCommand(_context, loggerContatoPessoaCommand);

            var contatosPessoas = contatosPessoasCommand.GetContatosPessoas();

            Assert.True(contatosPessoas?.Count() > 0);
        }

        [Fact]
        public void ValidaUpdateContatoPessoa() 
        {
            ContatosPessoasCommand contatosPessoasCommand = new ContatosPessoasCommand(_context, loggerContatoPessoaCommand);

            var contatoPessoa = contatosPessoasCommand.GetContatoPessoa(1);

            if(contatoPessoa is not null) 
            {
                contatoPessoa.Numero = "3256780900";

                Assert.True(contatosPessoasCommand.AlterarContatoPessoa(contatoPessoa, contatoPessoa.Id));
            }
          
        }

        [Fact]
        public void ValidaExcluirContatoPessoa()
        {
            ContatosPessoasCommand contatosPessoasCommand = new ContatosPessoasCommand(_context, loggerContatoPessoaCommand);

            var contatoPessoa = contatosPessoasCommand.GetContatoPessoa(2);

            if (contatoPessoa is not null)
            {
                Assert.True(contatosPessoasCommand.ExcluirContatoPessoa(contatoPessoa.Id));
            }

        }
    }
}
