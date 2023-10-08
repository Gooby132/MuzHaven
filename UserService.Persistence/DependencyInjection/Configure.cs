using DomainSeed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Domain.Repositories;
using UserService.Persistence.Context;
using UserService.Persistence.Repositories;

namespace UserService.Persistence.DependencyInjection;

public static class Configure
{

    public const string Key = "Persistence";

    public static IServiceCollection ConfigureUserPersistence(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        configuration = configuration.GetSection(Key);

        services.AddDbContext<UserContext>();

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserUnitOfWork, UnitOfWork>();

        return services;
    }

}
