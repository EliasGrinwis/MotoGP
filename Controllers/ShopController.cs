using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MotoGP.Data;
using MotoGP.Models;
using MotoGP.Models.ViewModels;

namespace MotoGP.Controllers
{
    public class ShopController : Controller
    {
        private readonly GPContext _context;

        public ShopController(GPContext context) 
        {
            _context = context;
        }
      
        public IActionResult Orderticket()
        {
            ViewData["BannerNr"] = 3;

            ViewData["Countries"] = new SelectList(_context.Countries.OrderBy(c => c.Name), "CountryID", "Name");
            ViewData["Races"] = _context.Races.OrderBy(c => c.Name);

            return View();
        }

        public IActionResult ConfirmOrder(int id)
        {
            ViewData["BannerNr"] = 3;

            var ticket = _context.Tickets.Include(t => t.Race).
                SingleOrDefault(t => t.TicketID == id);

            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrder([Bind("TicketID,Name,Email,Address,CountryID,RaceID,Number")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.OrderDate = DateTime.Now;
                ticket.Paid = false;
                _context.Add(ticket);
                _context.SaveChanges();
                return RedirectToAction("ConfirmOrder", new { id = ticket.TicketID });
            }

            return RedirectToAction("Orderticket");
        }

        public IActionResult ListTickets(int raceID = 0)
        {
            ViewData["BannerNr"] = 3;
            var selectTicketsVM = new ListTicketsViewModel();

            if (raceID != 0)
            {
                selectTicketsVM.Tickets = _context.Tickets
                    .Include(t => t.Country)
                    .Where(t => t.RaceID == raceID).ToList();
            } else
            {
                selectTicketsVM.Tickets = _context.Tickets.Include(t => t.Country).ToList();
            }

            selectTicketsVM.Races = new SelectList(_context.Races.OrderBy(r => r.Name), "RaceID", "Name");

            return View(selectTicketsVM);
        }

        public IActionResult EditTicket(int id)
        {
            ViewData["BannerNr"] = 3;

            var ticket = _context.Tickets.Include(t => t.Country).Include(t => t.Race).SingleOrDefault(t => t.TicketID == id);

            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTicket(Ticket ticket)
        {

            ViewData["BannerNr"] = 3;

            var ticketUpdate = _context.Tickets.Find(ticket.TicketID);
            ticketUpdate.Paid = ticket.Paid;

            _context.Update(ticketUpdate);
            _context.SaveChanges();

            return RedirectToAction("ListTickets");
        }

    }
}
