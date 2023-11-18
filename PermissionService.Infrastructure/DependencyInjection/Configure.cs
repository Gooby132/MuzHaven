using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PermissionService.Infrastructure.Authorization.JwtProvider.DependencyInjection;

namespace PermissionService.Infrastructure.DependencyInjection;

public static class Configure
{

    public const string Key = "PermissionService";

    public static IServiceCollection ConfigurePermissionInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        configuration = configuration.GetRequiredSection(Key);

        services.ConfigureJwtProvider(configuration);

        return services;    

    }

}
