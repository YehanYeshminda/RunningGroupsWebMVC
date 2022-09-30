using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunningGroupsWeb.Data;
using RunningGroupsWeb.Interfaces;
using RunningGroupsWeb.Models;
using RunningGroupsWeb.ViewModel;

namespace RunningGroupsWeb.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceInterface _raceInterface;
        private readonly ICloudinaryInterface _cloudinaryInterface;

        public RaceController(IRaceInterface raceInterface, ICloudinaryInterface cloudinaryInterface)
        {
            _raceInterface = raceInterface;
            _cloudinaryInterface = cloudinaryInterface;
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
        public async Task<IActionResult> Create(CreateRaceViewModel createRaceViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _cloudinaryInterface.AddPhotoAsync(createRaceViewModel.Image);

                var race = new Race
                {
                    Title = createRaceViewModel.Title,
                    Description = createRaceViewModel.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        City = createRaceViewModel.Address.City,
                        Street = createRaceViewModel.Address.Street,
                        State = createRaceViewModel.Address.State,
                    }
                };

                _raceInterface.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Unable to upload Image!");
            }

            return View(createRaceViewModel);

        }
    }
}
