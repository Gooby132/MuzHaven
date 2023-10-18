using Microsoft.EntityFrameworkCore;
using PermissionService.Domain.ProjectPermissions;
using PermissionService.Domain.UserPermissions;

namespace PermissionService.Persistence.Context;

public class PermissionContext : DbContext
{

    //public DbSet<UserProjectPermission> UserProjectPermissions { get; set; }
    public DbSet<UserPermission> UserPermissions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseInMemoryDatabase("permission");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<UserPermission>(builder =>
            {
                builder.HasKey(p => p.UserId);

                builder.Property(p => p.Permission)
                .HasConversion(
                    p => p.Value,
                    p => Domain.UserPermissions.ValueObjects.Permissions.FromValue(p));
            });

        modelBuilder
            .Entity<UserProjectPermission>(builder =>
            {
                builder.HasKey(p => new { p.UserId, p.ProjectId });

                builder.Property(p => p.Permission)
                .HasConversion(
                p => p.Value,
                p => Domain.ProjectPermissions.ValueObjects.Permissions.FromValue(p));

            });

    }

}
