using AlienProject.GenerateTables;
using AlienProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlienProject.Controllers
{
    public class KillingController : Controller
    {
        private readonly AlienDbContext _context;

        public KillingController(AlienDbContext context)
        {
            this._context = context;
        }
        public IActionResult Killing()
        {
            Generate generate = new(_context);
            var viewModelList = generate.GenerateKillingTable();
            return View(viewModelList); ;
        }

        [HttpGet]
        public IActionResult FindAlien()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FindAlien(string humanName,DateTime fromDate, DateTime toDate)
        {
            Generate generate = new(_context);
            var kills = generate.GenerateKillingTable();
            var killedAlien = kills
                .Where(a => a.HumanName == humanName && a.KillingDate >= fromDate && a.KillingDate <= toDate)
                .ToList();

            return View(killedAlien);
        }

    }
}
