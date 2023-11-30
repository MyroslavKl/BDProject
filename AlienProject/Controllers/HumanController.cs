using AlienProject.Models;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var human = _context.Humans.Where(s => s.HumanId == Id).FirstOrDefault();

            return View(human);

        }
        [HttpPost]
        public IActionResult Edit(Human human)
        {
            if (human == null)
            {
                return NotFound();
            }
            _context.Humans.Update(human);
            _context.SaveChanges();
            return RedirectToAction("Humans");
        }

    }
}
