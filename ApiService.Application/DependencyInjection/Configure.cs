using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PermissionService.Infrastructure.Authorization.JwtProvider.DependencyInjection;
using UserService.Persistence.DependencyInjection;
using ProjectService.Persistence.DependencyInjection;
using PermissionService.Persistence.DependencyInjection;
using PermissionService.Infrastructure.Authorization.JwtProvider.Swagger;

namespace ApiService.Application.DependencyInjection;

public static class Configure
{

    public static IServiceCollection ConfigureApiService(this IServiceCollection services, IConfiguration configuration)
    {

        services
            .ConfigureJwtProvider(configuration)
            .ConfigureProjectPersistence(configuration)
            .ConfigureUserPersistence(configuration)
            .ConfigurePermissionService(configuration)
            .ConfigureSwagger();

        services.AddMediatR(conf => conf.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

        return services;
    }

}
