using HotelBookingSystem.Models.Domain;

namespace HotelBookingSystem.Repositories.Abstract
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomType>> getRoomTypesAsync();

        Task<decimal> getRoomPriceAsync(int id);
    }
}
