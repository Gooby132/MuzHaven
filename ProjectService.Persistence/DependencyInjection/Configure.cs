using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectService.Domain.Repositories;
using ProjectService.Persistence.Context;
using ProjectService.Persistence.Repositories;

namespace ProjectService.Persistence.DependencyInjection;

public static class Configure
{
    public const string Key = "Projects";

    public static IServiceCollection ConfigureProjectPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        configuration = configuration.GetSection(Key);

        services.AddDbContext<ProjectContext>();

        services.AddTransient<IProjectUnitOfWork>(prov => new UnitOfWork(prov.GetRequiredService<ProjectContext>()));
        services.AddTransient<IProjectRepository, ProjectRepository>();

        return services;
    }

}
