using HotelBookingSystem.Models.Domain;

namespace HotelBookingSystem.Repositories.Abstract
{
    public interface IBranchService
    {
        Task<IEnumerable<HotelBranch>> GetHotelBranchesAsync();
    }
}
