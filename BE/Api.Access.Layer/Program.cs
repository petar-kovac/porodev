using Api.Access.Layer.Helpers.GlobalExceptionHandler;
using Api.Access.Layer.Mapper;
using Business.Access.Layer.Extensions;
using Business.Access.Layer.Mapper;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
    policy =>
    {
        policy.WithOrigins("http://localhost:3000/",
    "http://localhost:3000")
    .AllowAnyHeader()
    .AllowAnyMethod(); ;
    });
});
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

app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();

app.Run();