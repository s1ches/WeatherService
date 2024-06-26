using System.Reflection;
using ServiceC.Core.Application;
using ServiceC.Infrastructure.Grpc.Services;
using ServiceC.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddPersistenceLayer(configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGrpcService<WeatherInteractionService>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();