using PoroDev.Common.MassTransit;
using PoroDev.GatewayAPI.Helpers.GlobalExceptionHandler;
using PoroDev.GatewayAPI.Services;
using PoroDev.GatewayAPI.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransitWithRabbitMq();
builder.Services.AddScoped<IUserManagementService, UserManagementService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();