using FluentValidation.AspNetCore;
using Pharmacy_Backend.Controllers;
using Pharmacy_Backend.Repositories;

namespace Pharmacy_Backend.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddFluentValidation();

            //Services
            services.AddScoped<IlaclarController>();

            //Repositories
            services.AddScoped<IIlacRepositories,EfIlacRepository>();
            return services;

        }
    }
}
