using Microsoft.EntityFrameworkCore;
using tmsminimalapi.Models;

namespace tmsminimalapi.Data;

public class TmsDbContext : DbContext
{
    public TmsDbContext(DbContextOptions<TmsDbContext> options) : base(options)
    {
    }

    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Party> Parties { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Booking>()
            .Property(b => b.TotalAmount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Booking>()
            .Property(b => b.PaidByParty)
            .HasPrecision(18, 2);

        // Configure Party-Booking relationship
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Party)
            .WithMany(p => p.Bookings)
            .HasForeignKey(b => b.PartyId)
            .OnDelete(DeleteBehavior.Restrict);

        // Add index for party name search
        modelBuilder.Entity<Party>()
            .HasIndex(p => p.PartyName);
    }
} 