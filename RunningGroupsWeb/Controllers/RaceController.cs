using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunningGroupsWeb.Data;

namespace RunningGroupsWeb.Controllers
{
    public class RaceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RaceController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var races = _context.Races.ToList();
            return View(races);
        }

        public IActionResult Detail(int id)
        {
            // we only need the include if we have a object inside of the data
            var raceDetails = _context.Races.Include(a => a.Address).FirstOrDefault(c => c.Id == id);
            return View(raceDetails);
        }
    }
}
