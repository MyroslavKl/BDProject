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
    }
}
