using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Pharmacy_Backend.Helpers
{
    public static class SwaggerServiceExtensions
    {
        private static string PharmacyApiVersion = "v1";
        private static string PharmacyApiName = "Pharmacy API";
        private static string PharmacyApiDesc = "Welcome to Pharmacy API";

        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(PharmacyApiVersion, new OpenApiInfo
                {
                    Version = PharmacyApiVersion,
                    Title = PharmacyApiName,
                    Description = PharmacyApiDesc
                });

                c.EnableAnnotations();

                // Authorize => Bearer <token>  
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. Example: Authorization: Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                Array.Empty<string>()
                }
                });
            });



            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", PharmacyApiName);
                c.DocumentTitle = PharmacyApiDesc;
                c.DocExpansion(DocExpansion.None);
            });
            return app;
        }
    }
}
