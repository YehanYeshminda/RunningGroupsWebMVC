using RunningGroupsWeb.Models;

namespace RunningGroupsWeb.Interfaces
{
    public interface IRaceInterface
    {
        bool Add(Race race);

        bool Delete(Race race);

        bool Update(Race race);

        bool Save();

        Task<IEnumerable<Race>> GetAllRaces();

        Task<Race> GetByIdAsync(int id);

        Task<IEnumerable<Race>> GetAllRacesByCity(string city);
    }
}
