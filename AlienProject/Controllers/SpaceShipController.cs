using AlienProject.GenerateTables;
using AlienProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlienProject.Controllers
{
    public class SpaceShipController : Controller
    {
        private readonly AlienDbContext _context;

        public SpaceShipController(AlienDbContext context) {
            this._context = context;
        }
        public IActionResult SpaceShips()
        {
            var spaceShips = _context.Spaceshipps.ToList();
            return View(spaceShips);
        }
        [HttpGet]
        public IActionResult FindPeople() {
            return View();
        }
        [HttpPost]
        public IActionResult FindPeople(string humanName, DateTime fromDate, DateTime toDate) {
            Generate generate = new(_context);
            var data = generate.GenerateSpaceShipTable();
            var spaceshipsVisited = data
            .Where(visit => visit.HumanName == humanName && visit.SpaceshipVisitDate >= fromDate && visit.SpaceshipVisitDate <= toDate)
            .Select(visit => visit.SpaceshipName)
            .ToList();
            return View(spaceshipsVisited);
        }

    }
}
