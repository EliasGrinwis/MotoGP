using Microsoft.AspNetCore.Mvc.Rendering;

namespace MotoGP.Models.ViewModels
{
    public class SelectRaceViewModel
    {
        public Race race;
        public SelectList Races { get; set; }
        public int raceID { get; set; }
    }
}
