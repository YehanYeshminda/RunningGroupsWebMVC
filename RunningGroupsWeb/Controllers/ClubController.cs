using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunningGroupsWeb.Data;
using RunningGroupsWeb.Interfaces;
using RunningGroupsWeb.Models;
using RunningGroupsWeb.ViewModel;

namespace RunningGroupsWeb.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubInterface _clubInterface;
        private readonly ICloudinaryInterface _cloudinaryInterface;

        public ClubController(IClubInterface clubInterface, ICloudinaryInterface cloudinaryInterface)
        {
            _clubInterface = clubInterface;
            _cloudinaryInterface = cloudinaryInterface;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel createClubViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _cloudinaryInterface.AddPhotoAsync(createClubViewModel.Image);

                var club = new Club
                {
                    Title = createClubViewModel.Title,
                    Description = createClubViewModel.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        City = createClubViewModel.Address.City,
                        Street = createClubViewModel.Address.Street,
                        State = createClubViewModel.Address.State
                    }
                };
                _clubInterface.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo Upload Failed!");
            }

            return View(createClubViewModel);

        }
    }
}
