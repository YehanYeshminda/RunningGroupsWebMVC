using Microsoft.AspNetCore.Mvc;
using RunningGroupsWeb.Data;

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
    }
}
