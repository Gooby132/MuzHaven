using DomainSeed.ValueObjects.Internet;
using Microsoft.EntityFrameworkCore;
using PermissionService.Domain.ProjectPermissions;
using PermissionService.Domain.UserPermissions;
using PermissionService.Domain.UserPermissions.ValueObjects;

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
                builder.HasKey(p => p.Email);

                builder.Property(p => p.Email)
                    .HasConversion(
                    p => p.Raw,
                    p => Email.Create(p).Value);

                builder.Property(p => p.Password)
                    .HasConversion(
                    p => p.Text,
                    p => Password.Create(p).Value);

                builder.Property(p => p.Permission)
                .HasConversion(
                    p => p.Value,
                    p => Permissions.FromValue(p));
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
