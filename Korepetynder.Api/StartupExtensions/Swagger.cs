using System.Reflection;
using Korepetynder.Api.OperationFilters;
using Microsoft.OpenApi.Models;

namespace Korepetynder.Api.StartupExtensions
{
    public static class Swagger
    {
        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
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

                // Azure AD B2C support
                options.AddSecurityDefinition("aad-jwt", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(configuration["AzureAdB2C:Instance"] +
                                $"/{configuration["AzureAdB2C:Domain"]}/oauth2/v2.0/authorize?p=" +
                                configuration["AzureAdB2C:SignUpSignInPolicyId"]),
                            TokenUrl = new Uri(configuration["AzureAdB2C:Instance"] +
                                $"/{configuration["AzureAdB2C:Domain"]}/oauth2/v2.0/token?p=" +
                                configuration["AzureAdB2C:SignUpSignInPolicyId"]),
                            Scopes = new Dictionary<string, string>
                            {
                                { "openid", "Sign in permissions" },
                                { $"{configuration["Swagger:AppIdUrl"]}/Api.Access", "API permissions" }
                            }
                        },
                    }
                });

                options.OperationFilter<OAuthSecurityRequirementOperationFilter>();
            });
        }

        public static void ConfigureSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Korepetynder API v1");
                options.OAuthClientId(configuration["Swagger:AdClientId"]);
                options.OAuthUsePkce();
            });
        }
    }
}

