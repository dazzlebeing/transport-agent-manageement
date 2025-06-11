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
    public DbSet<Truck> Trucks { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<BookingTruckAssignment> BookingTruckAssignments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Booking>()
            .Property(b => b.TotalAmount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Booking>()
            .Property(b => b.PaidByParty)
            .HasPrecision(18, 2);

        modelBuilder.Entity<BookingTruckAssignment>()
            .Property(b => b.AgreedAmount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<BookingTruckAssignment>()
            .Property(b => b.AdvancePaid)
            .HasPrecision(18, 2);

        // Configure Party-Booking relationship
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Party)
            .WithMany(p => p.Bookings)
            .HasForeignKey(b => b.PartyId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure Booking-Assignment relationship
        modelBuilder.Entity<BookingTruckAssignment>()
            .HasOne(b => b.Booking)
            .WithMany()
            .HasForeignKey(b => b.BookingId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure Truck-Assignment relationship
        modelBuilder.Entity<BookingTruckAssignment>()
            .HasOne(b => b.Truck)
            .WithMany(t => t.BookingAssignments)
            .HasForeignKey(b => b.TruckId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure Driver-Assignment relationship
        modelBuilder.Entity<BookingTruckAssignment>()
            .HasOne(b => b.Driver)
            .WithMany(d => d.BookingAssignments)
            .HasForeignKey(b => b.DriverId)
            .OnDelete(DeleteBehavior.Restrict);

        // Add indexes
        modelBuilder.Entity<Party>()
            .HasIndex(p => p.PartyName);

        modelBuilder.Entity<Truck>()
            .HasIndex(t => t.TruckNumber)
            .IsUnique();

        modelBuilder.Entity<Driver>()
            .HasIndex(d => d.DriverMobile)
            .IsUnique();
    }
} 