using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunningGroupsWeb.Data;
using RunningGroupsWeb.Models;

namespace RunningGroupsWeb.Controllers
{
    public class ClubController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClubController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var clubs = _context.Clubs.ToList(); // sending data into the view
            return View(clubs);
        }

        // details page
        public IActionResult Detail(int id)
        {
            // we only need the include if we have a object inside of the data
            var clubDetails = _context.Clubs.Include(a => a.Address).FirstOrDefault(c => c.Id == id);
            return View(clubDetails);
        }
    }
}
