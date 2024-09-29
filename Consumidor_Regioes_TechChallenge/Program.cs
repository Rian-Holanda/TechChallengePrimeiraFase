using Consumidor_Regioes_TechChallenge;
using DataAccess_TechChallengePrimeiraFase.Contatos.Command;
using DataAccess_TechChallengePrimeiraFase.Contatos.Interface;
using DataAccess_TechChallengePrimeiraFase.Contatos.Interfaces;
using DataAccess_TechChallengePrimeiraFase.Contatos.Queries;
using DataAccess_TechChallengePrimeiraFase.Regioes.Command;
using DataAccess_TechChallengePrimeiraFase.Regioes.Interface;
using DataAccess_TechChallengePrimeiraFase.Regioes.Queries;
using Infrastructure_TechChallengePrimeiraFase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);

var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .SetBasePath(Directory.GetCurrentDirectory())
                .Build();


builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("ConnectionString")));


builder.Services.AddScoped<IRegiaoCommand, RegiaoCommand>();
builder.Services.AddScoped<IRegiaoCodigoAreaCommand, RegiaoCodigoAreaCommand>();

builder.Services.AddHostedService<WorkerRegiaoService>();


var host = builder.Build();
host.Run();
