using Microsoft.EntityFrameworkCore;
using PoroDev.Common.MassTransit;
using PoroDev.Database.Data;
using PoroDev.Database.MapperProfiles;
using PoroDev.Database.Repositories;
using PoroDev.Database.Repositories.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<SqlDataContext>(options =>
            {
    options.UseMySql(
       builder.Configuration.GetConnectionString("DefaultConnection"),
       ServerVersion.AutoDetect(    builder.Configuration.GetConnectionString("DefaultConnection"))
       );
});

builder.Services.AddMassTransitWithRabbitMq();
builder.Services.AddAutoMapper(typeof(MapperProfiles));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
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