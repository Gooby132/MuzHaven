using DomainSeed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StemService.Domain.Repositories;
using StemService.Persistence.Context;
using StemService.Persistence.Repositories;

namespace StemService.Persistence.DependencyInjection;

public static class Configure
{

    public const string Key = "Stems";

    public static IServiceCollection ConfigureStemPersistence(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        configuration = configuration.GetSection(Key);

        services.AddDbContext<StemContext>();

        services.AddTransient<IStemRepository, StemRepository>();
        services.AddTransient<IStemUnitOfWork, UnitOfWork>();

        return services;
    }

}
