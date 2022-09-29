using Microsoft.AspNetCore.Mvc;

namespace RunningGroupsWeb.Controllers
{
    public class RaceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
