using RunningGroupsWeb.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RunningGroupsWeb.Models
{
    public class Club
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        [ForeignKey("Address")] // foreign key which means take the primary key from the address and then take the primary key
        public int AddressId { get; set; }

        public Address Address { get; set; } // we pass in the whole address object

        public ClubCatergory ClubCatergory { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }

        public AppUser? AppUser { get; set; } // passing in the whole user of the club

    }
}
