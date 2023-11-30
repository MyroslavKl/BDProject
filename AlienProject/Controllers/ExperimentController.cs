using AlienProject.Additional;
using AlienProject.GenerateTables;
using AlienProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlienProject.Controllers
{
    public class ExperimentController : Controller
    {
        private readonly AlienDbContext _context;

        public ExperimentController(AlienDbContext context)
        {
                this._context = context;
        }
        public IActionResult Experiments()
        {
            Generate generate = new(_context);
            var experiment = generate.GenerateExperimentsTable();
            return View(experiment);
        }
        [HttpGet]
        public IActionResult FindExperimentsForHuman()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FindExperimentsForHuman(string personName, int minAliensCount, DateTime fromDate, DateTime toDate)
        {
            var experimentsInfo = _context.Experiments
           .Where(e => e.Human.Name == personName && e.ExperimentDate >= fromDate && e.ExperimentDate <= toDate)
           .GroupBy(e => e.ExperimentId)
           .Select(g => new ExperimentInfo
           {
               ExperimentId = g.Key,
               ExperimentDate = g.Max(e => e.ExperimentDate),
               AlienCount = g.Select(e => e.AlienId).Distinct().Count(),
           })
           .Where(info => info.AlienCount >= minAliensCount)
           .ToList();

            return View(experimentsInfo);
        }

    }
}
