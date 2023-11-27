using AlienProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}
