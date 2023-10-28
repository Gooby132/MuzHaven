using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserServiceClient.Abstractions;
using UserServiceClient.HttpClient.Core;
using UserServiceClient.HttpClient.Options;

namespace UserServiceClient.HttpClient.DependencyInjection;

public static class Configure
{
    public const string Key = "HttpClient";

    public static IServiceCollection ConfigureUserServiceHttpClient(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        configuration = configuration.GetRequiredSection(Key);

        services
            .AddOptions<HttpClientOptions>()
            .Bind(configuration.GetSection(HttpClientOptions.Key))
            .ValidateDataAnnotations();

        services
            .AddHttpClient<IUserServiceClient, UserServiceHttpClient>();

        return services;
    }
}
