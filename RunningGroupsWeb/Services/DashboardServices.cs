using RunningGroupsWeb.Data;
using RunningGroupsWeb.Interfaces;
using RunningGroupsWeb.Models;

namespace RunningGroupsWeb.Services
{
    public class DashboardServices : IDashboardInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardServices(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Club>> GetAllClubs()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userClubs = _context.Clubs.Where(r => r.AppUser.Id == currentUser);
            return userClubs.ToList();
        }

        public async Task<List<Race>> GetAllUserRaces()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userRaces = _context.Races.Where(r => r.AppUser.Id == currentUser);
            return userRaces.ToList();
        }
    }
}
