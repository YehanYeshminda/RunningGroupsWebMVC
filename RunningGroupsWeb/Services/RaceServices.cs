using Microsoft.EntityFrameworkCore;
using RunningGroupsWeb.Data;
using RunningGroupsWeb.Interfaces;
using RunningGroupsWeb.Models;

namespace RunningGroupsWeb.Services
{
    public class RaceServices : IRaceInterface
    {
        private readonly ApplicationDbContext _context;

        public RaceServices(ApplicationDbContext Context)
        {
            _context = Context;
        }

        public bool Add(Race race)
        {
            _context.Add(race);
            return Save();
        }

        public bool Delete(Race race)
        {
            _context.Remove(race);
            return Save();
        }

        public async Task<IEnumerable<Race>> GetAllRaces()
        {
            var races = await _context.Races.ToListAsync();
            return races;
        }

        public async Task<IEnumerable<Race>> GetAllRacesByCity(string city)
        {
            var racesByCity = await _context.Races.Where(c => c.Address.City.Contains(city)).ToListAsync();
            return racesByCity;
        }

        public async Task<Race> GetByIdAsync(int id)
        {
            var singleRace = await _context.Races.Include(e => e.Address).FirstOrDefaultAsync(e => e.Id == id);
            return singleRace;
        }

        public async Task<Race> GetByIdAsyncNoTracking(int id)
        {
            var singleRace = await _context.Races.Include(e => e.Address).AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            return singleRace;
        }

        public bool Update(Race race)
        {
            _context.Update(race);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
