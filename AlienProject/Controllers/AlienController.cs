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
        public IActionResult Add() {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Alien alien) {
            if (alien == null)
            {
                return View();
            }


            _context.Aliens.Add(alien);
            _context.SaveChanges();
            return RedirectToAction("Aliens");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var alien = _context.Aliens.Where(s => s.AlienId == Id).FirstOrDefault();

            return View(alien);

        }
        [HttpPost]
        public IActionResult Edit(Alien alien)
        {
            if (alien == null) {
                return NotFound();
            }
            _context.Aliens.Update(alien);
            _context.SaveChanges();
            return RedirectToAction("Aliens");
        }
    }
}
