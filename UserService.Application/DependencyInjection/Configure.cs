using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace UserService.Application.DependencyInjection;

public static class Configure
{
    public const string Key = "Application";

    public static IServiceCollection ConfigureApplication(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        configuration = configuration.GetSection(Key);

        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssemblies(
                AppDomain.CurrentDomain.GetAssemblies());
        });

        return services;
    }

}
