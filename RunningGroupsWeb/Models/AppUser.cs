namespace RunningGroupsWeb.Models
{
    public class AppUser
    {

        public int? Pace { get; set; }
        public int? MileAge { get; set; }
        public Address? Address { get; set; } // 1 to many

    }
}
