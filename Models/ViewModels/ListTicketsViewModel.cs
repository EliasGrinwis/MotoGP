using Microsoft.AspNetCore.Mvc.Rendering;

namespace MotoGP.Models.ViewModels
{
    public class ListTicketsViewModel
    {
        public List<Ticket> Tickets;
        public SelectList Races { get; set; }
        public int raceID { get; set; }
    }
}
