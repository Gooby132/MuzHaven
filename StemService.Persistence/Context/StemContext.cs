using Microsoft.EntityFrameworkCore;
using StemService.Domain;

namespace StemService.Persistence.Context;

internal class StemContext : DbContext
{

    public DbSet<Stem> Stems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseInMemoryDatabase("stems");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}
