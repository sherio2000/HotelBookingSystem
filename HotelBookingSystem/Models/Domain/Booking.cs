using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models.Domain
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [Required]
        public int BranchId { get; set; }

        [ForeignKey("BranchId")]
        public HotelBranch Branch { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        [Required]
        public int NumberOfRooms { get; set; }

        [Required]
        public bool DiscountApplied { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalCost { get; set; }

        public ICollection<Room> Rooms { get; set; }
    }
}
