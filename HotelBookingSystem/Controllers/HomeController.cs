using HotelBookingSystem.Models;
using HotelBookingSystem.Models.Domain;
using HotelBookingSystem.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace HotelBookingSystem.Controllers
{

    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IRoomService _roomRepository;
        private readonly IBranchService _branchRepository;

        public HomeController(ILogger<HomeController> logger, IRoomService roomRepository, IBranchService branchRepository)
        {
            _logger = logger;
            _roomRepository = roomRepository;
            _branchRepository = branchRepository;
        }

        public async Task populateRoomTypesList()
        {
            IEnumerable<RoomType> roomTypes = await _roomRepository.getRoomTypesAsync();
            IEnumerable<SelectListItem> listItems = roomTypes.Select(roomType => new SelectListItem
            {
                Text = roomType.TypeName,
                Value = roomType.RoomTypeId.ToString()
            });
            ViewBag.RoomTypes = listItems;
        }

        public async Task populateBranchList()
        {
            IEnumerable<HotelBranch> branches = await _branchRepository.GetHotelBranchesAsync();
            IEnumerable<SelectListItem> listItems = branches.Select(branch => new SelectListItem
            {
                Text = branch.BranchName,
                Value = branch.BranchId.ToString()
            });
            ViewBag.Branches = listItems;
        }


        public async Task<IActionResult> Index()
        {
            await populateRoomTypesList();
            await populateBranchList();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
