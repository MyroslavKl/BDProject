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
    }
}
