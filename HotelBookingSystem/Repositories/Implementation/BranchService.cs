using HotelBookingSystem.Models.Domain;
using HotelBookingSystem.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace HotelBookingSystem.Repositories.Implementation
{
    public class BranchService : IBranchService
    {

        private readonly HotelDbContext _context;

        public BranchService(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HotelBranch>> GetHotelBranchesAsync()
        {
            return await _context.HotelBranches.ToListAsync();
        }
    }
}
