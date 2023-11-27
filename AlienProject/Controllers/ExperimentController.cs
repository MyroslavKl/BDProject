using AlienProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlienProject.Controllers
{
    public class ExperimentController : Controller
    {
        private readonly AlienDbContext _context;

        public ExperimentController(AlienDbContext context)
        {
                this._context = context;
        }
        public IActionResult Experiments()
        {
            var query = from experiment in _context.Experiments
                        join alien in _context.Aliens on experiment.AlienId equals alien.AlienId
                        join human in _context.Humans on experiment.HumanId equals human.HumanId into humanGroup
                        from human in humanGroup.DefaultIfEmpty() // Use left join for humans
                        select new
                        {
                            ExperimentId = experiment.ExperimentId,
                            ExperimentDate = experiment.ExperimentDate,
                            HumanId = experiment.HumanId,
                            AlienId = experiment.AlienId,
                            AlienName = alien.Name,
                            AlienBirthDate = alien.BirthDate,
                            AlienEmail = alien.Email,
                            AlienPassword = alien.Password,
                            HumanName = human != null ? human.Name : null,
                            HumanBirthDate = human != null ? human.BirthDate : null,
                            HumanEmail = human != null ? human.Email : null,
                            HumanPassword = human != null ? human.Password : null
                        };

            var viewModelList = query.Select(result => new ExperimentViewModel
            {
                ExperimentId = result.ExperimentId,
                ExperimentDate = result.ExperimentDate,
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
