using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Persistence.DependencyInjection;
using ProjectService.Persistence.DependencyInjection;
using PermissionService.Persistence.DependencyInjection;
using PermissionService.Infrastructure.Authorization.JwtProvider.Swagger;
using StemService.Persistence.DependencyInjection;
using PermissionService.Infrastructure.DependencyInjection;
using StemService.Infrastructure.DependencyInjection;

namespace ApiService.Application.DependencyInjection;

public static class Configure
{

    public static IServiceCollection ConfigureApiService(this IServiceCollection services, IConfiguration configuration)
    {

        services
            .ConfigureProjectPersistence(configuration)
            .ConfigureUserPersistence(configuration)
            .ConfigurePermissionPersistence(configuration)
            .ConfigurePermissionInfrastructure(configuration)
            .ConfigureStemPersistence(configuration)
            .ConfigureStemInfrastructure(configuration)
            .ConfigureStemSwagger()
            .ConfigurePersistenceSwagger();

        services.AddMediatR(conf => conf.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

        return services;
    }

}
