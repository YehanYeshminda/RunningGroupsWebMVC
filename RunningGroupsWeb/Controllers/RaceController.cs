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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RaceController(IRaceInterface raceInterface, ICloudinaryInterface cloudinaryInterface, IHttpContextAccessor httpContextAccessor)
        {
            _raceInterface = raceInterface;
            _cloudinaryInterface = cloudinaryInterface;
            _httpContextAccessor = httpContextAccessor;
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
            var currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();

            var createViewModel = new CreateRaceViewModel
            {
                AppUserId = currentUserId
            };
            return View(createViewModel);
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
                    AppUserId = createRaceViewModel.AppUserId,
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

        public async Task<IActionResult> Edit(int id)
        {
            var race = await _raceInterface.GetByIdAsync(id);

            if (race == null) return View("Error");

            var raceVm = new EditRaceViewModel
            {
                Title = race.Title,
                Description = race.Description,
                AddressId = race.AddressId,
                Address = race.Address,
                RaceCatergory = race.RaceCatergory,
            };

            return View(raceVm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRaceViewModel editRaceViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Unable to edit race");
                return View("Edit", editRaceViewModel);
            }

            var raceModel = await _raceInterface.GetByIdAsyncNoTracking(id);

            if (raceModel != null)
            {
                try
                {
                    await _cloudinaryInterface.DeletePhotoAsync(raceModel.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Unable to delete photo");
                    return View(editRaceViewModel);
                }

                var photoResult = await _cloudinaryInterface.AddPhotoAsync(editRaceViewModel.Image);

                var race = new Race
                {
                    Id = id,
                    Title = editRaceViewModel.Title,
                    Description = editRaceViewModel.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = editRaceViewModel.AddressId,
                    Address = editRaceViewModel.Address
                };

                _raceInterface.Update(race);

                return RedirectToAction("Index");
            }
            else
            {
                return View(editRaceViewModel);
            }

        }

    }
}
