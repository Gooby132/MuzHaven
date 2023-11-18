using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StemService.Domain.Services;
using StemService.Infrastructure.FileServices.LocalFileService.Core;
using StemService.Infrastructure.FileServices.LocalFileService.Options;
using StemService.Domain.DependencyInjection;

namespace StemService.Infrastructure.FileServices.LocalFileService.DependencyInjection;

public static class Configure
{

    public const string Key = "LocalFileConfigurations";

    public static IServiceCollection ConfigureLocalFileProvider(this IServiceCollection services, IConfiguration configuration)
    {

        configuration = configuration.GetRequiredSection(Key);

        services.ConfigureStemDomain(configuration);
        services.AddTransient<IFileService, LocalFileSystemService>();

        services
            .AddOptions<LocalFileServiceOptions>()
            .Bind(configuration.GetRequiredSection(LocalFileServiceOptions.Key))
            .ValidateDataAnnotations();

        return services;
    }

}
