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
            var data = GetCollection();
            var spaceshipsVisited = data
            .Where(visit => visit.HumanName == humanName && visit.SpaceshipVisitDate >= fromDate && visit.SpaceshipVisitDate <= toDate)
            .Select(visit => visit.SpaceshipName)
            .ToList();
            return View(spaceshipsVisited);
        }


        public IEnumerable<SpaceshipViewModel> GetCollection()
        {
            var query = from visit in _context.SpaceshippVisits
                        join spaceship in _context.Spaceshipps on visit.SpaceshipId equals spaceship.SpaceshipId
                        join human in _context.Humans on visit.HumanId equals human.HumanId
                        select new
                        {
                            visit.SpaceshipVisitId,
                            visit.SpaceshipId,
                            visit.SpaceshipVisitDate,
                            visit.HumanId,
                            HumanName = human.Name,
                            SpaceshipName = spaceship.SpaceshipName
                        };

            IEnumerable<SpaceshipViewModel> result = query.Select(res => new SpaceshipViewModel
            {
                SpaceshipVisitId = res.SpaceshipVisitId,
                SpaceshipId = res.SpaceshipId,
                SpaceshipVisitDate = res.SpaceshipVisitDate,
                SpaceshipName = res.SpaceshipName,
                HumanId = res.HumanId,
                HumanName = res.HumanName

            });
            return result;
        }

    }
}
