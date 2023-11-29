using AlienProject.Additional;
using AlienProject.GenerateTables;
using AlienProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlienProject.Controllers
{
    public class ExcursionController : Controller
    {
        private readonly AlienDbContext _context;

        public ExcursionController(AlienDbContext context)
        {
            this._context = context;
        }
        public IActionResult Excursion()
        {
            Generate generate = new(_context);
            var excursion = generate.GenerateExcursionTable();
            return View(excursion);
        }

        [HttpGet]
        public IActionResult FindExcursionsForAlien()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FindExcursionsForAlien(string alienName, int minPeopleCount, DateTime fromDate, DateTime toDate)
        {
            var excursionsInfo = _context.Excursions
         .Where(e => e.Alien.Name == alienName && e.ExcursionDate >= fromDate && e.ExcursionDate <= toDate)
         .GroupBy(e => e.ExcursionId)
         .Select(g => new ExcursionInfo
         {
             ExcursionId = g.Key,
             ExcursionDate = g.Max(e => e.ExcursionDate),
             AlienId = g.Max(e => e.AlienId),
             AlienName = g.Max(e => e.Alien.Name),
             HumanId = g.Max(e => e.HumanId),
             HumanName = g.Max(e => e.Human.Name),
             PeopleCount = g.Count()
         })
         .Where(info => info.PeopleCount >= minPeopleCount)
         .ToList();
            return View(excursionsInfo);
        }

    }
}
