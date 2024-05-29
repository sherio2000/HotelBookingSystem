using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models.DTO
{
    public class CustomerDto
    {
        
        public string Name { get; set; }

        public string NationalId { get; set; }
        public string PhoneNumber { get; set; }
    }

}
