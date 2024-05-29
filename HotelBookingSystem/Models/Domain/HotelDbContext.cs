using HotelBookingSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

public class HotelDbContext : DbContext
{
    public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<HotelBranch> HotelBranches { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HotelBranch>()
    .HasMany(b => b.Bookings)
    .WithOne(b => b.Branch)
    .HasForeignKey(b => b.BranchId);

        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Bookings)
            .WithOne(b => b.Customer)
            .HasForeignKey(b => b.CustomerId);

        modelBuilder.Entity<Booking>()
            .HasMany(b => b.Rooms)
            .WithOne(r => r.Booking)
            .HasForeignKey(r => r.BookingId);
    }
}
