using AlienProject.GenerateTables;
using AlienProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlienProject.Controllers
{
    public class AbductionController : Controller
    {
        private readonly AlienDbContext _context;

        public AbductionController(AlienDbContext context)
        {
            this._context = context;
        }
        public IActionResult Abduction()
        {
            Generate generate = new(_context);
            var viewModelList = generate.GeneretingAbductionTable();
            return View(viewModelList);
        }
        [HttpGet]
        public IActionResult Find()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Find(string alienName, int minAbductionCount, DateTime fromDate, DateTime toDate)
        {
            Generate generate = new(_context);
            var abduct = generate.GeneretingAbductionTable();
            var abductedPeople = abduct
                .Where(a => a.AlienName == alienName && a.AbductionDate >= fromDate && a.AbductionDate <= toDate)
                .GroupBy(a => a.HumanName)
                .Where(g => g.Count() >= minAbductionCount)
                .Select(g => g.Key)
                .ToList();

            return View(abductedPeople);
        }

        [HttpGet]
        public IActionResult FindAlien()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FindAlien(string humanName, int minAbductionCount, DateTime fromDate, DateTime toDate)
        {
            Generate generate = new(_context);
            var abduct = generate.GeneretingAbductionTable();
            var abductedPeople = abduct
                .Where(a => a.HumanName == humanName && a.AbductionDate >= fromDate && a.AbductionDate <= toDate)
                .GroupBy(a => a.AlienName)
                .Where(g => g.Count() >= minAbductionCount)
                .Select(g => g.Key)
                .ToList();

            return View(abductedPeople);
        }

        [HttpGet]
        public IActionResult FindKilledAlien()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FindKilledAlien(string humanName)
        {
            Generate generate = new(_context);
            var abduct = generate.GeneretingAbductionTable();
            var killed = generate.GenerateKillingTable();
            var abductedAndKilledAliens = abduct
                .Where(visit => visit.HumanName == humanName)
                .Where(alien => killed.Any(killing => killing.AlienId == alien.AlienId && killing.HumanName == humanName));

            var alienNames = abductedAndKilledAliens.Select(alien => alien.AlienName).ToList();

            return View(alienNames);
        }


        [HttpGet]
        public IActionResult FindDifferentAliens()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FindDifferentAliens(int minAbductionCount, DateTime fromDate, DateTime toDate)
        {
            Generate generate = new(_context);
            var data = generate.GeneretingAbductionTable();
            var aliensAbductingMultiplePeople = data
                .Where(abduction => abduction.AbductionDate >= fromDate && abduction.AbductionDate <= toDate)
                .GroupBy(abduction => abduction.AlienId)
                .Where(group => group.Count() >= minAbductionCount)
                .Select(group => data.First(abduction => abduction.AlienId == group.Key))
                .ToList();

            var alienNames = aliensAbductingMultiplePeople.Select(alien => alien.AlienName).ToList();

            return View(alienNames);
        }

    }
}
