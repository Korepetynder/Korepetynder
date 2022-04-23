using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Korepetynder.Api.OperationFilters
{
    public class OAuthSecurityRequirementOperationFilter : IOperationFilter
    {
        private readonly string appIdUrl;

        public OAuthSecurityRequirementOperationFilter(IConfiguration configuration)
        {
            appIdUrl = configuration["Swagger:AppIdUrl"];
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Get custom attributes on action and controller
            object[] attributes = context.ApiDescription.CustomAttributes().ToArray();
            if (attributes.OfType<AllowAnonymousAttribute>().Any())
            {
                // Controller / action allows anonymous calls
                return;
            }

            AuthorizeAttribute[] authorizeAttributes = attributes.OfType<AuthorizeAttribute>().ToArray();
            if (authorizeAttributes.Length == 0)
            {
                return;
            }
            operation.Security.Add(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "aad-jwt"
                        },
                        UnresolvedReference = true
                    },
                    new[]
                    {
                        $"{appIdUrl}/Api.Access"
                    }
                }
            });
        }
    }
}
