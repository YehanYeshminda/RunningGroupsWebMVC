using RunningGroupsWeb.Models;

namespace RunningGroupsWeb.Interfaces
{
    public interface IClubInterface
    {
        bool Add(Club club); // used to insert values

        bool Delete(Club club);

        bool Update(Club club);

        bool Save();

        Task<IEnumerable<Club>> GetAllClubs();

        Task<Club> GetByIdAsync(int id);

        Task<IEnumerable<Club>> GetClubByCity(string city);
    }
}
