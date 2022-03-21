using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Korepetynder.Api.StartupExtensions
{
    public static class Swagger
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Korepetynder API",
                    Version = "v1",
                    Description = "API for Korepetynder application",
                    Contact = new OpenApiContact
                    {
                        Name = "Konrad Krawiec",
                        Email = "kk429356@students.mimuw.edu.pl"
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Korepetynder API v1");
            });
        }
    }
}

