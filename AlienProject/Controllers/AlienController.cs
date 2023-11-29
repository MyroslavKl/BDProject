using AlienProject.Additional;
using AlienProject.GenerateTables;
using AlienProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AlienProject.Controllers
{
    public class AlienController : Controller
    {
        private readonly AlienDbContext _context;

        public AlienController(AlienDbContext context) {
            this._context = context;
        }
        public IActionResult Aliens()
        {
            var aliens = _context.Aliens;
            return View(aliens);
        }
        [HttpGet]
        public IActionResult FindCommon() {
            return View();
        }

        [HttpPost]
        public IActionResult FindCommon(string alienName, string humanName, DateTime fromDate, DateTime toDate) {
            Generate generate = new(_context);
            var excursions = generate.GenerateExcursionTable();
            var experiments = generate.GenerateExperimentsTable();
            var commonExcursions = excursions
       .Where(excursion => excursion.AlienName == alienName && excursion.HumanName == humanName
           && excursion.ExcursionDate >= fromDate && excursion.ExcursionDate <= toDate)
       .Select(excursion => new CommonActivityViewModel
       {
           ActivityId = excursion.ExcursionId,
           ActivityDate = excursion.ExcursionDate,
           AlienName = excursion.AlienName,
           AlienBirthDate = excursion.AlienBirthDate,
           AlienEmail = excursion.AlienEmail,
           HumanName = excursion.HumanName,
           HumanBirthDate = excursion.HumanBirthDate,
           HumanEmail = excursion.HumanEmail,
           Type = "Excursion"
       })
       .ToList();

            var commonExperiments = experiments
                .Where(experiment => experiment.AlienName == alienName && experiment.HumanName == humanName
                    && experiment.ExperimentDate >= fromDate && experiment.ExperimentDate <= toDate)
                .Select(experiment => new CommonActivityViewModel
                {
                    ActivityId = experiment.ExperimentId,
                    ActivityDate = experiment.ExperimentDate,
                    AlienName = experiment.AlienName,
                    AlienBirthDate = experiment.AlienBirthDate,
                    AlienEmail = experiment.AlienEmail,
                    HumanName = experiment.HumanName,
                    HumanBirthDate = experiment.HumanBirthDate,
                    HumanEmail = experiment.HumanEmail,
                    Type = "Experiment"
                })
                .ToList();

            var commonActivities = commonExcursions.Concat(commonExperiments).ToList();
            return View(commonActivities);

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
