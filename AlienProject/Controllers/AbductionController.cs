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


    }
}
