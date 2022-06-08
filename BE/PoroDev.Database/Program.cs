using Microsoft.EntityFrameworkCore;
using PoroDev.Database.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<SqlDataContext>(options =>
            {
    options.UseMySql(
       builder.Configuration.GetConnectionString("DefaultConnection"),
       ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
       );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}




app.Run();
