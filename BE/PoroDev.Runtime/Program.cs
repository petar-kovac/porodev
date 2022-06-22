using PoroDev.Common.MassTransit;
using PoroDev.Runtime.Extensions;
using PoroDev.Runtime.Extensions.Contracts;
using PoroDev.Runtime.Services;
using PoroDev.Runtime.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransitWithRabbitMq();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDockerWriter, DockerWriter>();
builder.Services.AddScoped<IRuntimeService, RuntimeService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
