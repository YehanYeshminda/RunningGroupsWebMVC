using Microsoft.AspNetCore.Mvc;
using RunningGroupsWeb.Data;
using RunningGroupsWeb.Interfaces;
using RunningGroupsWeb.ViewModel;

namespace RunningGroupsWeb.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDashboardInterface _dashboardInterface;

        public DashboardController(ApplicationDbContext context, IDashboardInterface dashboardInterface)
        {
            _context = context;
            _dashboardInterface = dashboardInterface;
        }
        public async Task<IActionResult> Index()
        {
            var userRaces = await _dashboardInterface.GetAllUserRaces();
            var userClubs = await _dashboardInterface.GetAllClubs();

            var userviewModel = new DashboardViewModel()
            {
                Races = userRaces,
                Clubs = userClubs
            };

            return View(userviewModel);
        }
    }
}
