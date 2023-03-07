using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MotoGP.Data;
using MotoGP.Models;
using MotoGP.Models.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MotoGP.Controllers
{
    public class Shop : Controller
    {

        private readonly GPContext _context;

        public Shop(GPContext context)
        {
            _context = context;
        }

        public IActionResult OrderTicket()
        {

            ViewData["BannerNr"] = 4;

            ViewData["Countries"] =
               new SelectList(_context.Countries.OrderBy(t => t.Name),
                              "CountryID",
                              "Name");
            ViewData["Races"] =
               new SelectList(_context.Races.OrderBy(r => r.Name),
                              "RaceID",
                              "Name");

            return View();
        }

        // POST : Shop/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OrderTicket([Bind("Name, Email, Address, CountryID, RaceID, Number")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {

                ticket.OrderDate = DateTime.Now;
                ticket.Paid = false;
                _context.Add(ticket);
                _context.SaveChanges();
                return RedirectToAction("ConfirmOrder", new { id = ticket.TicketID });
            }

            ViewData["BannerNr"] = 4;

            return View(ticket);
        }

        public IActionResult ConfirmOrder(int id)
        {
            ViewData["BannerNr"] = 4;

            var ticket = _context.Tickets.Include(t => t.Race)
                .SingleOrDefault(t => t.TicketID == id);

            return View(ticket);

        }

        public IActionResult ListTickets(int raceID = 0)
        {
            ViewData["BannerNr"] = 4;
            ViewData["raceNr"] = "/images/races/" + raceID + ".jpg";

            var listTicketsVM = new SelectRaceViewModel();

            if (raceID != 0)
            {
                listTicketsVM.TicketList = _context.Tickets.Where(m => m.RaceID == raceID).Include(m => m.Race).Include(m => m.Country).
                    OrderBy(r => r.Name).ToList();

            }
            else
            {
                listTicketsVM.TicketList = new List<Ticket>();
            }

            listTicketsVM.Races =
            new SelectList(_context.Races.OrderBy(r => r.Name),
                        "RaceID", "Name");
            listTicketsVM.raceID = raceID;

            return View(listTicketsVM);
        }

        public IActionResult EditTicket(int id)
        {
            ViewData["BannerNr"] = 4;

            var ticket = _context.Tickets.Include(t => t.Race).Include(t => t.Country).SingleOrDefault(t => t.TicketID == id);

            return View(ticket);        
        }

        // POST : Shop/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTicket(Ticket ticket)
        {
            ViewData["BannerNr"] = 4;

            var ticketUpdate = _context.Tickets.Find(ticket.TicketID);
            ticketUpdate.Paid = ticket.Paid;

            _context.Update(ticketUpdate);
            _context.SaveChanges();

            return RedirectToAction("ListTickets", new { raceID = ticketUpdate.RaceID });

        }
    }
}
