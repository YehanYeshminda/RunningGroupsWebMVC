using Microsoft.EntityFrameworkCore;
using RunningGroupsWeb.Data;
using RunningGroupsWeb.Interfaces;
using RunningGroupsWeb.Models;

namespace RunningGroupsWeb.Services
{
    public class ClubServices : IClubInterface
    {
        private readonly ApplicationDbContext _context;

        public ClubServices(ApplicationDbContext Context)
        {
            _context = Context;
        }


        public bool Add(Club club)
        {
            _context.Add(club);
            return Save(); // this is the save below
        }

        public bool Delete(Club club)
        {
            _context.Remove(club);
            return Save();
        }

        public bool Update(Club club)
        {
            _context.Update(club);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        // getting all the clubs
        public async Task<IEnumerable<Club>> GetAllClubs()
        {
            var clubs = await _context.Clubs.ToListAsync();
            return clubs;
        }

        // getting a single club and getting the foreign key value as well
        public async Task<Club> GetByIdAsync(int id)
        {
            var singleClub = await _context.Clubs.Include(e => e.Address).FirstOrDefaultAsync(e => e.Id == id);
            return singleClub;
        }

        // search for a city with a string provided
        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            var clubByCity = await _context.Clubs.Where(c => c.Address.City.Contains(city)).ToListAsync();
            return clubByCity;
        }


    }
}
