using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MotoGP.Data;
using MotoGP.Models;
using MotoGP.Models.ViewModels;

namespace MotoGP.Controllers
{
    public class Info : Controller
    {

        private readonly GPContext _context;

        public Info(GPContext context) {
            _context = context;
        }   

        public IActionResult ListRaces()
        {
            var races = _context.Races.OrderBy(race => race.Date);

            ViewData["BannerNr"] = 1;

            return View(races.ToList());
        }

        public IActionResult BuildMap()
        {
            ViewData["BannerNr"] = 1;

            var races = _context.Races;

            return View(races.ToList());
        }

        public IActionResult ShowRace(int id)
        {
            ViewData["BannerNr"] = 1;

            var race = _context.Races.SingleOrDefault(r => r.RaceID == id);
            ViewData["raceNr"] = "/images/races/" + id + ".jpg";

            return View(race);
        }

        public IActionResult ListRiders()
        {
            ViewData["BannerNr"] = 2;

            var riders = _context.Riders.OrderBy(rider => rider.Number);

            return View(riders.ToList());
        }

        public IActionResult SelectRace(int raceID = 0)
        {
            ViewData["BannerNr"] = 1;
            ViewData["raceNr"] = "/images/races/" + raceID + ".jpg";

            var listRacesVM = new SelectRaceViewModel();

            if (raceID != 0)
            {
                listRacesVM.RacesList = _context.Races.Where(m => m.RaceID == raceID).OrderBy(r => r.Name).ToList();

            } else
            {
                listRacesVM.RacesList = new List<Race>();
            }

            listRacesVM.Races =
            new SelectList(_context.Races.OrderBy(r => r.Name),
                        "RaceID", "Name");
            listRacesVM.raceID = raceID;

            return View(listRacesVM);
        }

        public IActionResult ListTeams()
        {
            ViewData["BannerNr"] = 3;

            var teams = _context.Teams.Include(rider => rider.Riders).OrderBy(team => team.Name);


            return View(teams.ToList());
        }

        public IActionResult ListTeamsRiders(int teamID = 0)
        {
            ViewData["BannerNr"] = 3;

            var listTeamsVM = new ListTeamsRidersViewModel();

            listTeamsVM.TeamsList = _context.Teams.OrderBy(r => r.Name).ToList();

            if (teamID != 0)
            {
                listTeamsVM.RidersList = _context.Riders.Where(r => r.TeamID == teamID).ToList();
            }
            else
            {
                listTeamsVM.RidersList = new List<Rider>();
            }

            /*            var teams = _context.Teams.Include(rider => rider.Riders).OrderBy(team => team.Name);
            */
            return View(listTeamsVM);
        }
    }
}
