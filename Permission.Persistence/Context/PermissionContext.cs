using Microsoft.EntityFrameworkCore;
using PermissionService.Domain.ProjectPermissions;
using PermissionService.Domain.UserPermissions;
using Ardalis.SmartEnum.EFCore;
using SmartEnum.EFCore;

namespace PermissionService.Persistence.Context;

public class PermissionContext : DbContext
{

    public DbSet<UserProjectPermission> UserProjectPermissions { get; set; }
    public DbSet<UserPermission> UserPermissions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseInMemoryDatabase("permission");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureSmartEnum();

        modelBuilder.Entity<UserProjectPermission>().OwnsOne(p => p.Permission).WithOwner();
        modelBuilder.Entity<UserPermission>().OwnsOne(p => p.Permission).WithOwner();

    }

}
