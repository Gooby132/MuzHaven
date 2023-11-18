using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StemService.Infrastructure.FileAuthorizer.JwtAuthorizer.DependencyInjection;
using StemService.Infrastructure.FileServices.LocalFileService.DependencyInjection;

namespace StemService.Infrastructure.DependencyInjection;

public static class Configure
{

    public const string Key = "StemService";

    public static IServiceCollection ConfigureStemInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        configuration = configuration.GetRequiredSection(Key);

        services.ConfigureJwtProvider(configuration);
        services.ConfigureLocalFileProvider(configuration);

        return services;
    }

}
