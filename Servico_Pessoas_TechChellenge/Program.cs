using Business_TechChallengePrimeiraFase.Contatos.Application.Interfaces;
using Business_TechChallengePrimeiraFase.Contatos.Domain;
using Business_TechChallengePrimeiraFase.Regioes.Application.Interfaces;
using Business_TechChallengePrimeiraFase.Regioes.Domain;
using DataAccess_TechChallengePrimeiraFase.Contatos.Command;
using DataAccess_TechChallengePrimeiraFase.Contatos.Interface;
using DataAccess_TechChallengePrimeiraFase.Contatos.Interfaces;
using DataAccess_TechChallengePrimeiraFase.Contatos.Queries;
using DataAccess_TechChallengePrimeiraFase.Regioes.Command;
using DataAccess_TechChallengePrimeiraFase.Regioes.Interface;
using DataAccess_TechChallengePrimeiraFase.Regioes.Queries;
using Infrastructure_TechChallengePrimeiraFase;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Prometheus;
using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Gateway.Interface;
using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Gateway.Producer;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .SetBasePath(Directory.GetCurrentDirectory())
                .Build();

builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("ConnectionString")));


builder.Services.AddScoped<IPessoaProducer, PessoaProducer>();
builder.Services.AddScoped<IContatosPessoasQueries, ContatosPessoasQueries>();
builder.Services.AddScoped<IContatosPessoasCommand, ContatosPessoasCommand>();
builder.Services.AddScoped<IPessoasCommand, PessoasCommand>();
builder.Services.AddScoped<IPessoasQueries, PessoasQueries>();
builder.Services.AddScoped<IRegiaoQueries, RegiaoQueries>();
builder.Services.AddScoped<IRegiaoCommand, RegiaoCommand>();
builder.Services.AddScoped<IRegiaoCodigoAreaCommand, RegiaoCodigoAreaCommand>();
builder.Services.AddScoped<IRegiaoCodigoAreaQueries, RegiaoCodigoAreaQueries>();
builder.Services.AddScoped<IValidaEmailPessoa, ValidaEmailPessoa>();
builder.Services.AddScoped<IValidacoesRegioes, ValidacoesRegioes>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseMetricServer();
app.UseHttpMetrics();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
