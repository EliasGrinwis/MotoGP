using Microsoft.AspNetCore.Mvc.Rendering;

namespace MotoGP.Models.ViewModels
{
    public class SelectRaceViewModel
    {
        public List<Race> RacesList;
        public List<Ticket> TicketList;
        public SelectList Races { get; set;}
        public SelectList Tickets { get; set; }
        public int raceID { get; set; }
    }
}
