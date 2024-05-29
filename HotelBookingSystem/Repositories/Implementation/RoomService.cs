using HotelBookingSystem.Repositories.Abstract;
using HotelBookingSystem.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Repositories.Implementation
{

    public class RoomService : IRoomService
    {

        private readonly HotelDbContext _context;

        public RoomService(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> getRoomPriceAsync(int id)
        {
            var room  = await _context.RoomTypes.FindAsync(id);
            return room.PricePerNight;
        }

        public async Task<IEnumerable<RoomType>> getRoomTypesAsync()
        {
            return await _context.RoomTypes.ToListAsync();
        }
    }
}
