using PoroDev.Common.MassTransit;
using PoroDev.UserManagementService.Mapper;
using PoroDev.UserManagementService.Services;
using PoroDev.UserManagementService.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(MapperProfiles));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddMassTransitWithRabbitMq();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();