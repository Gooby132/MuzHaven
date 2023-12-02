using Microsoft.EntityFrameworkCore;
using ProjectService.Domain;
using ProjectService.Domain.ValueObjects;

namespace ProjectService.Persistence.Context;

internal class ProjectContext : DbContext
{

    public DbSet<Project> Projects { get; set; }

    public ProjectContext()
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseInMemoryDatabase("projects");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<Project>()
            .OwnsOne(p => p.Title)
            .WithOwner();

        modelBuilder
            .Entity<Project>()
            .OwnsOne(p => p.Description)
            .WithOwner();

        modelBuilder
            .Entity<Project>()
            .OwnsOne(p => p.MusicalProfile)
            .WithOwner();
    }
}
