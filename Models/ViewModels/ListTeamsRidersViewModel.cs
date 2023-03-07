using Microsoft.AspNetCore.Mvc.Rendering;

namespace MotoGP.Models.ViewModels
{
    public class ListTeamsRidersViewModel
    {
        public List<Team> TeamsList;
        public List<Rider> RidersList;
        public SelectList Teams { get; set; }
        public SelectList Riders { get; set; }
        public int teamID { get; set; }
    }
}
