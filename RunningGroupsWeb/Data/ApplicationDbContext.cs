using Microsoft.EntityFrameworkCore;
using RunningGroupsWeb.Models;

namespace RunningGroupsWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        // contructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Race> Races { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
