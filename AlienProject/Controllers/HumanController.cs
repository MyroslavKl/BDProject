using AlienProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlienProject.Controllers
{
    public class HumanController : Controller
    {
        private readonly AlienDbContext _context;

        public HumanController(AlienDbContext context)
        {
            this._context = context;
        }
        public IActionResult Humans()
        {
            var humans= _context.Humans;
            return View(humans);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Human human)
        {
            if (human == null)
            {
                return View();
            }

            _context.Humans.Add(human);
            _context.SaveChanges();
            return RedirectToAction("Aliens");
        }

    }
}
