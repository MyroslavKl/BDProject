using AlienProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlienProject.Controllers
{
    public class KillingController : Controller
    {
        private readonly AlienDbContext _context;

        public KillingController(AlienDbContext context)
        {
            this._context = context;
        }
        public IActionResult Killing()
        {
            var viewModelList = Generate();
            return View(viewModelList); ;
        }

        [HttpGet]
        public IActionResult FindAlien()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FindAlien(string humanName,DateTime fromDate, DateTime toDate)
        {
            var kills = Generate();
            var killedAlien = kills
                .Where(a => a.HumanName == humanName && a.KillingDate >= fromDate && a.KillingDate <= toDate)
                .ToList();

            return View(killedAlien);
        }


        public IEnumerable<KillingViewModel> Generate() {
            var query = from killings in _context.Killings
                        join alien in _context.Aliens on killings.AlienId equals alien.AlienId
                        join human in _context.Humans on killings.HumanId equals human.HumanId into humanGroup
                        from human in humanGroup.DefaultIfEmpty()
                        select new
                        {
                            KillingId = killings.KillingId,
                            KillingDate = killings.KillingDate,
                            HumanId = killings.HumanId,
                            AlienId = killings.AlienId,
                            AlienName = alien.Name,
                            AlienBirthDate = alien.BirthDate,
                            AlienEmail = alien.Email,
                            AlienPassword = alien.Password,
                            HumanName = human != null ? human.Name : null,
                            HumanBirthDate = human != null ? human.BirthDate : null,
                            HumanEmail = human != null ? human.Email : null,
                            HumanPassword = human != null ? human.Password : null
                        };

            var viewModelList = query.Select(result => new KillingViewModel
            {
                KillingId = result.KillingId,
                KillingDate = result.KillingDate,
                HumanId = result.HumanId,
                AlienId = result.AlienId,
                AlienName = result.AlienName,
                AlienBirthDate = result.AlienBirthDate,
                AlienEmail = result.AlienEmail,
                HumanName = result.HumanName,
                HumanBirthDate = result.HumanBirthDate,
                HumanEmail = result.HumanEmail,
            }).ToList();
            return viewModelList;
        }
    }
}
