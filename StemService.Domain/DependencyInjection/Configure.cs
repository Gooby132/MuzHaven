using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StemService.Domain.DependencyInjection;

public static class Configure
{

    public static IServiceCollection ConfigureStemDomain(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }

}
