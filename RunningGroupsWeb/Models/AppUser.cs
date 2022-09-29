using System.ComponentModel.DataAnnotations;

namespace RunningGroupsWeb.Models
{
    public class AppUser
    {
        [Key]
        public string Id { get; set; }

        public int? Pace { get; set; }
        public int? MileAge { get; set; }
        public Address? Address { get; set; } // 1 user can have many addresses
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Race> Races { get; set; }

    }
}
