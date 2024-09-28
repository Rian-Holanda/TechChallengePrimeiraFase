using Consumidor_Regioes_TechChallenge;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<WorkerRegiao>();
builder.Services.AddHostedService<WorkerCodigoAreaRegiao>();

var host = builder.Build();
host.Run();
