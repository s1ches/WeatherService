using Hangfire;
using ServiceA.WorkerWeatherCollector.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddServices();
builder.Services.AddWorkers(configuration);

var app = builder.Build();

app.UseHangfireDashboard("/ServiceA/WorkerWeatherCollector/Dashboard");
app.UseRecurringJobs(configuration);

app.Run();
