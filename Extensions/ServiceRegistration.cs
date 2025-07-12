using FluentValidation.AspNetCore;
using Pharmacy_Backend.Controllers;

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



            return services;

        }
    }
}
