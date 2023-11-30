using AlienProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlienProject.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly AlienDbContext _context;
        public AuthenticationController(AlienDbContext context) {
            _context = context;
        }

        [HttpGet("login")]
        public IActionResult Login() {
            return View();
        }
    }
}
