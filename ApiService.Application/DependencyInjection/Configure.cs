using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PermissionService.Infrastructure.Authorization.JwtProvider.DependencyInjection;
using UserService.Persistence.DependencyInjection;

namespace ApiService.Application.DependencyInjection;

public static class Configure
{

    public static IServiceCollection ConfigureApiService(this IServiceCollection services, IConfiguration configuration)
    {

        services.ConfigureJwtProvider(configuration);
        services.ConfigureUserPersistence(configuration);

        services.AddMediatR(conf => conf.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

        return services;
    }

}
