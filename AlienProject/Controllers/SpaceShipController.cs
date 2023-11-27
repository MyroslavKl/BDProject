using AlienProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlienProject.Controllers
{
    public class SpaceShipController : Controller
    {
        private readonly AlienDbContext _context;

        public SpaceShipController(AlienDbContext context) {
            this._context = context;
        }
        public IActionResult SpaceShips()
        {
            var spaceShips = _context.Spaceshipps.ToList();
            return View(spaceShips);
        }
    }
}
