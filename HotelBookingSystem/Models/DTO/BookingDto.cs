using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models.DTO
{
    public class BookingDto
    {
        public CustomerDto Customer { get; set; }
        public int BranchId { get; set; }
        [Required(ErrorMessage = "Please enter your name.")]
        public DateTime CheckInDate { get; set; }
        [Required(ErrorMessage = "Please enter your name.")]
        public DateTime CheckOutDate { get; set; }
        [Required(ErrorMessage = "Please enter your name.")]
        public int numOfRooms { get; set; }
        public List<RoomDto> Rooms { get; set; }
    }
}
