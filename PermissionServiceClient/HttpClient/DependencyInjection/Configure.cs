using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PermissionServiceClient.Abstractions;
using PermissionServiceClient.HttpClient.Core;
using PermissionServiceClient.HttpClient.Options;

namespace PermissionServiceClient.HttpClient.DependencyInjection;

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
            .AddHttpClient<IPermissionServiceClient, PermissionServiceHttpClient>();

        return services;
    }
}
