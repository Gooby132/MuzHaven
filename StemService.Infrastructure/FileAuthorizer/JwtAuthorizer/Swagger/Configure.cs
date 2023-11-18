using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using StemService.Domain.Services;

namespace PermissionService.Infrastructure.Authorization.JwtProvider.Swagger;

public static class Configure
{

    public static IServiceCollection ConfigureStemSwagger(this IServiceCollection services)
    {

        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition(IFileAuthorizer.StemSchemeName, new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using stems request",
                Name = IFileAuthorizer.StemHeaderName,
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = IFileAuthorizer.StemSchemeName
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
          {
            {
              new OpenApiSecurityScheme
              {
                    Reference = new OpenApiReference
                      {
                        Type = ReferenceType.SecurityScheme,
                        Id = IFileAuthorizer.StemSchemeName
                      },
                      Scheme = "oauth2",
                      Name = IFileAuthorizer.StemHeaderName,
                      In = ParameterLocation.Header,

                    },
                    new List<string>()
                  }
            });

        });

        return services;
    }

}
