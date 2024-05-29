using HotelBookingSystem.Models.Domain;
using HotelBookingSystem.Models.DTO;
using HotelBookingSystem.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Linq;
using System.Threading.Tasks;


namespace HotelBookingSystem.Repositories.Implementation
{
    public class BookingService : IBookingService
    {

        private readonly HotelDbContext _context;

        public BookingService(HotelDbContext context)
        {
            _context = context;
        }


        public async Task<Booking> AddBookingAsync(BookingDto bookingDto)
        {
            // Find or create the customer
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.NationalId == bookingDto.Customer.NationalId);

            if (customer == null)
            {
                customer = new Customer
                {
                    Name = bookingDto.Customer.Name,
                    NationalId = bookingDto.Customer.NationalId,
                    PhoneNumber = bookingDto.Customer.PhoneNumber,
                    HasPreviousBooking = false
                };
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync(); // Save changes asynchronously
            }
            else
            {
                customer.HasPreviousBooking = true;
            }

            // Create the booking
            var booking = new Booking
            {
                CustomerId = customer.CustomerId,
                Customer = customer,
                BranchId = bookingDto.BranchId,
                CheckInDate = bookingDto.CheckInDate,
                CheckOutDate = bookingDto.CheckOutDate,
                NumberOfRooms = bookingDto.Rooms.Count,
                DiscountApplied = customer.HasPreviousBooking,
                TotalCost = 0,
                Rooms = bookingDto.Rooms.Select(r => new Room
                {
                    RoomTypeId = r.RoomTypeId,
                    NumberOfAdults = r.NumberOfAdults,
                    NumberOfChildren = r.NumberOfChildren,
                }).ToList(),
            };

            // Add rooms to the booking
            decimal totalCost = 0;

            foreach (var room in bookingDto.Rooms)
            {

                var selectedRoom = _context.RoomTypes.FirstOrDefault((rooms) => rooms.RoomTypeId == room.RoomTypeId);
                totalCost += selectedRoom.PricePerNight;
            }
            booking.TotalCost = totalCost;

            // Apply discount if applicable
            if (booking.DiscountApplied)
            {
                totalCost *= 0.95m; // Apply 5% discount
                booking.TotalCost = totalCost;

            }


            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync(); // Save changes asynchronously

            return booking;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsByNationalIdAsync(string nationalId)
        {
            var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.NationalId == nationalId);

            if (customer == null)
            {
                return Enumerable.Empty<Booking>();
            }

            return await _context.Bookings
                .Include(b => b.Rooms)
                .Include(b => b.Branch)
                .Where(b => b.CustomerId == customer.CustomerId)
                .ToListAsync();
        }

        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            var booking = await _context.Bookings
               .Include(b => b.Branch)
               .FirstOrDefaultAsync(c => c.BookingId == id);

            if (booking == null)
            {
                throw new Exception("Booking not found");
            }

            // Fetch associated rooms separately
            var rooms = await _context.Rooms
                .Include(r => r.RoomType)
                .Where(r => r.BookingId == id)
                .ToListAsync();

            // Create a new Booking object and set its properties
            var bookingWithRooms = new Booking
            {
                BookingId = booking.BookingId,
                CustomerId = booking.CustomerId,
                Customer = booking.Customer,
                BranchId = booking.BranchId,
                Branch = booking.Branch,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                NumberOfRooms = booking.NumberOfRooms,
                DiscountApplied = booking.DiscountApplied,
                TotalCost = booking.TotalCost,
                Rooms = rooms
            };

            return bookingWithRooms;
        }

        public async Task<int> GetBookingsCountByNationalIdAsync(string nationalId)
        {
            var count = await _context.Bookings.CountAsync(b => b.Customer.NationalId == nationalId);
            return count;
        }
    }
    
}
