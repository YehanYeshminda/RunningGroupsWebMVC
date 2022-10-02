using RunningGroupsWeb.Data.Enums;
using RunningGroupsWeb.Models;

namespace RunningGroupsWeb.ViewModel
{
    public class CreateRaceViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public RaceCatergory RaceCatergory { get; set; }
        public string AppUserId { get; set; }
    }
}
