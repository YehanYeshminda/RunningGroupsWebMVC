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
        public readonly IHttpContextAccessor _httpContextAccessor;

        public ClubController(IClubInterface clubInterface, ICloudinaryInterface cloudinaryInterface, IHttpContextAccessor httpContextAccessor)
        {
            _clubInterface = clubInterface;
            _cloudinaryInterface = cloudinaryInterface;
            _httpContextAccessor = httpContextAccessor;
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
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var clubModel = new CreateClubViewModel
            {
                AppUserId = currentUserId
            };
            return View(clubModel);
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
                    AppUserId = createClubViewModel.AppUserId,
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


        // edit data get
        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubInterface.GetByIdAsync(id);

            if (club == null) return View("Error");

            var clubVm = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AddressId,
                Address = club.Address,
                URL = club.Image,
                ClubCatergory = club.ClubCatergory
            };

            return View(clubVm);
        }

        [HttpPost] // edit post
        public async Task<IActionResult> Edit(int id, EditClubViewModel editClubViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", editClubViewModel);
            }

            var clubModel = await _clubInterface.GetByIdAsyncNoTracking(id);

            if (clubModel != null)
            {
                try
                {
                    await _cloudinaryInterface.DeletePhotoAsync(clubModel.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Unable to delete photo!");
                    return View(editClubViewModel);
                }

                var photoResult = await _cloudinaryInterface.AddPhotoAsync(editClubViewModel.Image);

                var club = new Club
                {
                    Id = id,
                    Title = editClubViewModel.Title,
                    Description = editClubViewModel.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = editClubViewModel.AddressId,
                    Address = editClubViewModel.Address,
                };

                _clubInterface.Update(club);

                return RedirectToAction("Index");


            }
            else
            {
                return View(editClubViewModel);
            }
        }
    }
}
