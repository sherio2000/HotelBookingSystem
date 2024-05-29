using HotelBookingSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public static class DbInitializer
{
    public static void Initialize(HotelDbContext context)
    {
        context.Database.Migrate();

        if (context.HotelBranches.Any() || context.RoomTypes.Any())
        {
            return; // Database has already been seeded
        }

        var branches = new[]
        {
            new HotelBranch { BranchName = "Cairo" },
            new HotelBranch { BranchName = "Sharqiyah" },
            new HotelBranch { BranchName = "Hurghada" },
            new HotelBranch { BranchName = "Sharm El-Sheikh" },
            new HotelBranch { BranchName = "Ain El-Soukhna" },
            new HotelBranch { BranchName = "North Coast" },
            new HotelBranch { BranchName = "Dahab" }
        };

        context.HotelBranches.AddRange(branches);
        context.SaveChanges();

        var roomTypes = new[]
        {
            new RoomType { TypeName = "Single", PricePerNight = 100.00m },
            new RoomType { TypeName = "Double", PricePerNight = 150.00m },
            new RoomType { TypeName = "Triple", PricePerNight = 200.00m },
            new RoomType { TypeName = "Studio", PricePerNight = 250.00m },
            new RoomType { TypeName = "Suite", PricePerNight = 300.00m },
            new RoomType { TypeName = "Penthouse", PricePerNight = 400.00m },
        };

        context.RoomTypes.AddRange(roomTypes);
        context.SaveChanges();
    }
}
