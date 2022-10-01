using RunningGroupsWeb.Models;

namespace RunningGroupsWeb.Interfaces
{
    public interface IDashboardInterface
    {
        Task<List<Race>> GetAllUserRaces();
        Task<List<Club>> GetAllClubs();
    }
}
