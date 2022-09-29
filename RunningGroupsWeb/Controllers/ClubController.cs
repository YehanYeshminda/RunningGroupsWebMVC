using Microsoft.AspNetCore.Mvc;

namespace RunningGroupsWeb.Controllers
{
    public class ClubController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
