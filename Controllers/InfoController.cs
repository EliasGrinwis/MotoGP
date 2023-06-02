using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MotoGP.Data;
using MotoGP.Models;
using MotoGP.Models.ViewModels;
using System.Xml.Linq;

namespace MotoGP.Controllers
{
    public class InfoController : Controller
    {

        private readonly GPContext _context;

        public InfoController(GPContext context)
        {
            _context = context;
        }

        public IActionResult ListRaces()
        {
            ViewData["BannerNr"] = 0;

            var races = _context.Races.OrderBy(r => r.Date);

            return View(races.ToList());
        }

        public IActionResult BuildMap()
        {
            ViewData["BannerNr"] = 0;

            var races = _context.Races;

            return View(races);
        }

        public IActionResult ShowRace(int id)
        {
            ViewData["BannerNr"] = 0;

            var race = _context.Races.SingleOrDefault(r => r.RaceID == id);

            return View(race);
        }

        public IActionResult ListRiders()
        {
            ViewData["BannerNr"] = 0;

            var riders = _context.Riders.OrderBy(r => r.Number);

            return View(riders.ToList());
        }

        public IActionResult SelectRace(int raceID = 0)
        {
            ViewData["BannerNr"] = 0;
            var selectRacesVM = new SelectRaceViewModel();

            if (raceID != 0)
            {
                selectRacesVM.race = _context.Races.SingleOrDefault(r => r.RaceID == raceID);
            } else
            {
                selectRacesVM.race = null;
            }

            selectRacesVM.Races = new SelectList(_context.Races.OrderBy(r => r.Name), "RaceID", "Name");

            return View(selectRacesVM);
        }

        public IActionResult ListTeams()
        {
            ViewData["BannerNr"] = 2;

            var teams = _context.Teams.Include(t => t.riders).OrderBy(r => r.Name).ToList();

            return View(teams);
        }

        public IActionResult ListTeamsRiders(int teamID = 0)
        {
            ViewData["BannerNr"] = 2;
            var listTeamsRidersVM = new ListTeamsRidersViewModel();


            if (teamID != 0)
            {
                listTeamsRidersVM.Riders = _context.Riders.Where(r => r.TeamID == teamID).ToList();
            } else
            {
                listTeamsRidersVM.Riders = null;
            }

            listTeamsRidersVM.Teams = _context.Teams.OrderBy(t => t.Name).ToList();

            return View(listTeamsRidersVM);
        }
    }
}
