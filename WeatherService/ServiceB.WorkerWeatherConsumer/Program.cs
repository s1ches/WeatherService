using ServiceB.WorkerWeatherConsumer.Configuration;
using ServiceB.WorkerWeatherConsumer.Workers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddServices();
builder.Services.AddHostedService<KazanWeatherWorker>();

var host = builder.Build();
host.Run();