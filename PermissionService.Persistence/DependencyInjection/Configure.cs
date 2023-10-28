using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PermissionService.Domain.UserPermissions.Repositories;
using PermissionService.Domain.UserPermissions.UnitOfWork;
using PermissionService.Persistence.Context;
using PermissionService.Persistence.Repositories;

namespace PermissionService.Persistence.DependencyInjection;

public static class Configure
{

    public static IServiceCollection ConfigurePermissionService(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<PermissionContext>();

        services.AddTransient<IUserPermissionUnitOfWork, UnitOfWork>();
        services.AddTransient<IUserPermissionRepository, UserPermissionRepository>();

        return services;
    }

}
