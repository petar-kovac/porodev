using Microsoft.EntityFrameworkCore;
using PoroDev.Common.MassTransit;
using PoroDev.DatabaseService.Data;
using PoroDev.DatabaseService.Data.Configuration;
using PoroDev.DatabaseService.MapperProfiles;
using PoroDev.DatabaseService.Repositories;
using PoroDev.DatabaseService.Repositories.Contracts;
using PoroDev.DatabaseService.Services;
using PoroDev.DatabaseService.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<SqlDataContext>(options =>
            {
                options.UseMySql(
                   builder.Configuration.GetConnectionString("DefaultConnection"),
                   ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
                   );
            });

builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDB"));

builder.Services.AddMassTransitWithRabbitMq();
builder.Services.AddAutoMapper(typeof(MapperProfiles));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<IStorageRepository, StorageRepository>();
builder.Services.AddScoped<IRuntimeDataRepository, RuntimeDataRepository>();
builder.Services.AddScoped<IEncryptionService, AesEncryptionService>();
builder.Services.AddScoped<ISharedSpaceRepository, SharedSpaceRepository>();
builder.Services.AddScoped<ISharedSpacesUsersRepository, SharedSpacesUsersRepository>();
builder.Services.AddScoped<ISharedSpacesWithFilesRepository, SharedSpacesFilesRepository>();
builder.Services.AddScoped<IUserReportsRepository, UserReportsRepository>();

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