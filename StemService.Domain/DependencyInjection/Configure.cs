using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StemService.Domain.Factories;

namespace StemService.Domain.DependencyInjection;

public static class Configure
{

    public static IServiceCollection ConfigureStemDomain(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<StemPersistenceService>();

        return services;
    }

}
