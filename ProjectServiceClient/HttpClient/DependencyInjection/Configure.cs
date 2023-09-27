using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectServiceClient.Abstractions;
using ProjectServiceClient.HttpClient.Core;
using ProjectServiceClient.HttpClient.Options;

namespace ProjectServiceClient.HttpClient.DependencyInjection;

public static class Configure
{
    public const string Key = "HttpClient";

    public static IServiceCollection ConfigurePermissionServiceHttpClient(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        configuration = configuration.GetRequiredSection(Key);

        services
            .AddOptions<HttpClientOptions>()
            .Bind(configuration.GetSection(HttpClientOptions.Key))
            .ValidateDataAnnotations();

        services
            .AddHttpClient<IProjectServiceClient, ProjectServiceHttpClient>();

        return services;
    }
}
