using Api.Access.Layer.Helpers.GlobalExceptionHandler;
using Api.Access.Layer.Mapper;
using Business.Access.Layer.Extensions;
using Business.Access.Layer.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

SqlConnector.ConnectToSqlServer(builder.Services, builder.Configuration);

DependencyInjector.InjectDependencies(builder.Services);

builder.Services.AddAutoMapper(typeof(MapperBALProfiles), typeof(ApiProfiles));

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