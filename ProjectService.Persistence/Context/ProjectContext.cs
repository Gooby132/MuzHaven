using Microsoft.EntityFrameworkCore;
using ProjectService.Domain;
using ProjectService.Domain.ValueObjects;

namespace ProjectService.Persistence.Context;

internal class ProjectContext : DbContext
{

    public DbSet<Project> Projects { get; set; }

    public ProjectContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        //optionsBuilder.UseSqlite("Data Source=Projects;Mode=Memory");
        optionsBuilder.UseInMemoryDatabase("Projects");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<Project>()
            .OwnsOne(p => p.Title, builder =>
            {
                builder.Property(a => a.Text).IsRequired();
                builder.HasIndex(a => a.Text).IsUnique();
            });

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
