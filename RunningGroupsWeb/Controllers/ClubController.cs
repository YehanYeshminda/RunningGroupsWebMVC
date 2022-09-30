using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunningGroupsWeb.Data;
using RunningGroupsWeb.Interfaces;
using RunningGroupsWeb.Models;

namespace RunningGroupsWeb.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubInterface _clubInterface;

        public ClubController(IClubInterface clubInterface)
        {
            _clubInterface = clubInterface;
        }

        public async Task<IActionResult> Index()
        {
            var clubs = await _clubInterface.GetAllClubs(); // getting all the clubs using the interface
            return View(clubs);
        }

        public async Task<IActionResult> Detail(int id)
        {
            // we only need the include if we have a object inside of the data
            var clubDetails = await _clubInterface.GetByIdAsync(id);
            return View(clubDetails);
        }
    }
}
