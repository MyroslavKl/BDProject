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

            var viewModelList = query.Select(result => new AbductionViewModel
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
            return View(viewModelList);
        }
    }
}
