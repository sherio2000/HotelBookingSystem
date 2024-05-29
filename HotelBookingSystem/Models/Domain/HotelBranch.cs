using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models.Domain
{
    public class HotelBranch
    {
        [Key]
        public int BranchId { get; set; }

        [Required]
        [StringLength(100)]
        public string BranchName { get; set; }

        public ICollection<Booking> Bookings { get; set; }

    }
}
