using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StemService.Domain.Services;
using StemService.Infrastructure.FileServices.LocalFileService.Core;
using StemService.Infrastructure.FileServices.LocalFileService.Options;

namespace StemService.Infrastructure.FileServices.LocalFileService.DependencyInjection;

public static class Configure
{

    public const string Key = "LocalFileConfigurations";

    public static IServiceCollection ConfigureLocalFileProvider(this IServiceCollection services, IConfiguration configuration)
    {

        configuration = configuration.GetRequiredSection(Key);

        services.AddTransient<IFileService, LocalFileSystemService>();

        services
            .AddOptions<LocalFileServiceOptions>()
            .Bind(configuration.GetRequiredSection(LocalFileServiceOptions.Key))
            .ValidateDataAnnotations();

        return services;
    }

}
