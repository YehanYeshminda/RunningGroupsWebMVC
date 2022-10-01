using Microsoft.AspNetCore.Mvc;
using RunningGroupsWeb.Data;
using RunningGroupsWeb.Interfaces;

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


            return View();
        }
    }
}
