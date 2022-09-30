using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunningGroupsWeb.Data;
using RunningGroupsWeb.Interfaces;
using RunningGroupsWeb.Models;

namespace RunningGroupsWeb.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceInterface _raceInterface;

        public RaceController(IRaceInterface raceInterface)
        {
            _raceInterface = raceInterface;
        }

        public async Task<IActionResult> Index()
        {
            var races = await _raceInterface.GetAllRaces();
            return View(races);
        }

        public async Task<IActionResult> Detail(int id)
        {
            // we only need the include if we have a object inside of the data
            var raceDetails = await _raceInterface.GetByIdAsync(id);
            return View(raceDetails);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Race race)
        {
            if (!ModelState.IsValid)
            {
                return View(race);
            }

            _raceInterface.Add(race);
            return RedirectToAction("Index");
        }
    }
}
