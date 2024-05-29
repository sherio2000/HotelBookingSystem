using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models.Domain
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string NationalId { get; set; }

        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        public bool HasPreviousBooking { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
