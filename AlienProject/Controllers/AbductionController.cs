using AlienProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlienProject.Controllers
{
    public class AbductionController : Controller
    {
        private readonly AlienDbContext _context;

        public AbductionController(AlienDbContext context)
        {
            this._context = context;
        }
        public IActionResult Abduction()
        {
            var viewModelList = GeneretingAbductionTable();
            return View(viewModelList);
        }
        [HttpGet]
        public IActionResult Find()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Find(string alienName, int minAbductionCount, DateTime fromDate, DateTime toDate)
        {
            var abduct = GeneretingAbductionTable();
            var abductedPeople = abduct
                .Where(a => a.AlienName == alienName && a.AbductionDate >= fromDate && a.AbductionDate <= toDate)
                .GroupBy(a => a.HumanName)
                .Where(g => g.Count() >= minAbductionCount)
                .Select(g => g.Key)
                .ToList();

            return View(abductedPeople);
        }

        [HttpGet]
        public IActionResult FindAlien()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FindAlien(string humanName, int minAbductionCount, DateTime fromDate, DateTime toDate)
        {
            var abduct = GeneretingAbductionTable();
            var abductedPeople = abduct
                .Where(a => a.HumanName == humanName && a.AbductionDate >= fromDate && a.AbductionDate <= toDate)
                .GroupBy(a => a.AlienName)
                .Where(g => g.Count() >= minAbductionCount)
                .Select(g => g.Key)
                .ToList();

            return View(abductedPeople);
        }



        public IEnumerable<AbductionViewModel> GeneretingAbductionTable() {
            var query = from abduct in _context.Abductions
                        join alien in _context.Aliens on abduct.AlienId equals alien.AlienId
                        join human in _context.Humans on abduct.HumanId equals human.HumanId into humanGroup
                        from human in humanGroup.DefaultIfEmpty()
                        select new
                        {
                            AbducttionId = abduct.AbductionId,
                            AbductionDate = abduct.AbductionDate,
                            HumanId = abduct.HumanId,
                            AlienId = abduct.AlienId,
                            AlienName = alien.Name,
                            AlienBirthDate = alien.BirthDate,
                            AlienEmail = alien.Email,
                            AlienPassword = alien.Password,
                            HumanName = human != null ? human.Name : null,
                            HumanBirthDate = human != null ? human.BirthDate : null,
                            HumanEmail = human != null ? human.Email : null,
                            HumanPassword = human != null ? human.Password : null
                        };

            IEnumerable<AbductionViewModel> viewModelList = query.Select(result => new AbductionViewModel
            {
                AbductionId = result.AbducttionId,
                AbductionDate = result.AbductionDate,
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
