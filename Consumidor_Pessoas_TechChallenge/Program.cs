using Consumidor_Pessoas_TechChallenge;
using DataAccess_TechChallengePrimeiraFase.Contatos.Command;
using DataAccess_TechChallengePrimeiraFase.Contatos.Interface;
using DataAccess_TechChallengePrimeiraFase.Regioes.Command;
using DataAccess_TechChallengePrimeiraFase.Regioes.Interface;
using Infrastructure_TechChallengePrimeiraFase;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .SetBasePath(Directory.GetCurrentDirectory())
                .Build();


builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("ConnectionString")));


builder.Services.AddScoped<IPessoasCommand, PessoasCommand>();
builder.Services.AddScoped<IContatosPessoasCommand, ContatosPessoasCommand>();

builder.Services.AddHostedService<WorkerPessoaService>();

var host = builder.Build();
host.Run();
