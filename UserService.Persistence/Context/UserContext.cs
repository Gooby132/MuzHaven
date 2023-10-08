using Microsoft.EntityFrameworkCore;
using UserService.Domain;

namespace UserService.Persistence.Context;

internal class UserContext : DbContext
{

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseInMemoryDatabase("users");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().OwnsOne(u => u.MetaData).WithOwner();
        modelBuilder.Entity<User>().OwnsOne(u => u.ArtistDescription).WithOwner();

    }

}
