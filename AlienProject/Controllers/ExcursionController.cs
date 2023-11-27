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
            var query = from excursion in _context.Excursions
                        join alien in _context.Aliens on excursion.AlienId equals alien.AlienId
                        join human in _context.Humans on excursion.HumanId equals human.HumanId into humanGroup
                        from human in humanGroup.DefaultIfEmpty() 
                        select new
                        {
                            ExcursionId = excursion.ExcursionId,
                            ExcursionDate = excursion.ExcursionDate,
                            HumanId = excursion.HumanId,
                            AlienId = excursion.AlienId,
                            AlienName = alien.Name,
                            AlienBirthDate = alien.BirthDate,
                            AlienEmail = alien.Email,
                            AlienPassword = alien.Password,
                            HumanName = human != null ? human.Name : null,
                            HumanBirthDate = human != null ? human.BirthDate : null,
                            HumanEmail = human != null ? human.Email : null,
                            HumanPassword = human != null ? human.Password : null
                        };

            var viewModelList = query.Select(result => new ExcursionViewModel
            {
                ExcursionId = result.ExcursionId,
                ExcursionDate = result.ExcursionDate,
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
