using Business.Access.Layer.Services;
using Business.Access.Layer.Services.Contracts;
using Data.Access.Layer.Repositories;
using Data.Access.Layer.Repositories.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Access.Layer.Extensions
{
    public static class DependencyInjector
    {
        public static void InjectDependencies(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}