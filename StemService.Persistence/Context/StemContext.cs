﻿using Microsoft.EntityFrameworkCore;
using StemService.Domain;
using StemService.Domain.Entities.Comments;
using StemService.Domain.ValueObjects;

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

        modelBuilder.Entity<Stem>()
            .OwnsMany(stem => stem.Comments)
            .OwnsOne(comment => comment.Commenter)
            .WithOwner();

        modelBuilder.Entity<Stem>()
            .OwnsOne(stem => stem.Desciption)
            .WithOwner();

        modelBuilder.Entity<Stem>()
            .OwnsOne(stem => stem.MusicFile)
            .OwnsMany(mf => mf.AmplitudesPoints);

    }

}
