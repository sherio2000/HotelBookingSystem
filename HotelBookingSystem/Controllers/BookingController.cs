using HotelBookingSystem.Models.Domain;
using HotelBookingSystem.Models.DTO;
using HotelBookingSystem.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingRepository;
        private readonly IRoomService _roomService;
        private readonly HotelDbContext _hotelDbContext;

        public BookingController(IBookingService bookingRepository, IRoomService roomService, HotelDbContext hotelDbContext)
        {
            _bookingRepository = bookingRepository;
            _roomService = roomService;
            _hotelDbContext = hotelDbContext;
        }


        [HttpPost]
        public async Task<ActionResult<Booking>> AddBooking(BookingDto bookingDto)
        {

            try
            {


                await _bookingRepository.AddBookingAsync(bookingDto);

                return Ok(new { success = true, message = "Booking added successfully" });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // Return a meaningful error message
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the booking.");
            }



            //var booking = await _bookingRepository.AddBookingAsync(bookingDto);
            //return CreatedAtAction(nameof(GetBooking), new { id = booking.BookingId }, booking);
        }

        public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookingsByNationalId(string nationalId)
        {
            var bookings = await _bookingRepository.GetAllBookingsByNationalIdAsync(nationalId);
            if (bookings == null || !bookings.Any())
            {
                return NotFound("No bookings found for the provided National ID.");
            }

            var bookingDtos = bookings.Select(b => new
            {
                b.BookingId,
                CustomerName = b.Customer.Name,
                b.CheckInDate,
                b.CheckOutDate,
                b.NumberOfRooms,
                b.DiscountApplied,
                b.TotalCost
            });

            return Ok(bookingDtos);
        }

        public async Task<ActionResult<Booking>> GetBookingById(int id)
        {
            try
            {
                var booking = await _bookingRepository.GetBookingByIdAsync(id);
                if (booking == null)
                {
                    return NotFound();
                }

                // Project the data to a simpler object to avoid circular reference issues and reduce payload size
                var result = new
                {
                    Branch = booking.Branch.BranchName,
                    Rooms = booking.Rooms.Select(r => new
                    {
                        r.RoomType.TypeName,
                        r.RoomType.PricePerNight,
                        r.NumberOfAdults,
                        r.NumberOfChildren
                    }).ToList(),
                    booking.CheckInDate,
                    booking.CheckOutDate
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<int>> GetBookingCount(string nationalId)
        {
            try
            {
                var count = await _bookingRepository.GetBookingsCountByNationalIdAsync(nationalId);
                return Ok(count);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpGet]
        public async Task<ActionResult<decimal>> getRoomPrice(int roomTypeId)
        {
            try
            {
                var price = await _roomService.getRoomPriceAsync(roomTypeId);
                return Ok(price);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        public IActionResult Track()
        {

            return View();
        }
    }
}
