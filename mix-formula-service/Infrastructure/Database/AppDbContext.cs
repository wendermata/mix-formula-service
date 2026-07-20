using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Hench> Henches => Set<Hench>();
    public DbSet<Item> Items => Set<Item>();
    public DbSet<Formula> Formulas => Set<Formula>();
    public DbSet<Map> Maps => Set<Map>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Formula>(entity =>
        {
            entity.HasOne(f => f.SourceHench1)
                .WithMany()
                .HasForeignKey(f => f.SourceHench1Id)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(f => f.SourceHench2)
                .WithMany()
                .HasForeignKey(f => f.SourceHench2Id)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(f => f.TargetHench)
                .WithMany()
                .HasForeignKey(f => f.TargetHenchId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Map>()
            .HasMany(m => m.Henches)
            .WithMany(h => h.Maps)
            .UsingEntity(j => j.ToTable("MapHenches"));
    }
}
