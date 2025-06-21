using FindOutOurHouse.DAL.Places;
using Microsoft.EntityFrameworkCore;

namespace FindOutOurHouse.DAL;

public class EfDbContext(DbContextOptions<EfDbContext> options) : DbContext(options)
{
    public DbSet<Place> Places { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Place>(b =>
        {
            b.ToTable("Places");

            b.HasKey(k => k.Id);
            b.Property(p => p.Title).IsRequired().HasMaxLength(50);
        });
    }
}
