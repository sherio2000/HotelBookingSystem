using HotelBookingSystem.Models.Domain;
using HotelBookingSystem.Models.DTO;

namespace HotelBookingSystem.Repositories.Abstract
{
    public interface IBookingService
    {
        Task<Booking> AddBookingAsync(BookingDto bookingDto);
        Task<IEnumerable<Booking>> GetAllBookingsByNationalIdAsync(string nationalId);
        Task<int> GetBookingsCountByNationalIdAsync(string nationalId);
        Task<Booking> GetBookingByIdAsync(int id);
    }
}
