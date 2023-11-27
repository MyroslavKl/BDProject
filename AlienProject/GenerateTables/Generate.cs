using AlienProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlienProject.GenerateTables
{
    public class Generate
    {
        private readonly AlienDbContext _context;

        public Generate(AlienDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<AbductionViewModel> GeneretingAbductionTable()
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

        public IEnumerable<KillingViewModel> GenerateKillingTable()
        {
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
        public IEnumerable<ExcursionViewModel> GenerateExcursionTable()
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
            return viewModelList;
        }

        public IEnumerable<ExperimentViewModel> GenerateExperimentsTable()
        {
            var query = from experiment in _context.Experiments
                        join alien in _context.Aliens on experiment.AlienId equals alien.AlienId
                        join human in _context.Humans on experiment.HumanId equals human.HumanId into humanGroup
                        from human in humanGroup.DefaultIfEmpty() 
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
            return viewModelList;
        }
        public IEnumerable<SpaceshipViewModel> GenerateSpaceShipTable()
        {
            var query = from visit in _context.SpaceshippVisits
                        join spaceship in _context.Spaceshipps on visit.SpaceshipId equals spaceship.SpaceshipId
                        join human in _context.Humans on visit.HumanId equals human.HumanId
                        select new
                        {
                            visit.SpaceshipVisitId,
                            visit.SpaceshipId,
                            visit.SpaceshipVisitDate,
                            visit.HumanId,
                            HumanName = human.Name,
                            SpaceshipName = spaceship.SpaceshipName
                        };

            IEnumerable<SpaceshipViewModel> result = query.Select(res => new SpaceshipViewModel
            {
                SpaceshipVisitId = res.SpaceshipVisitId,
                SpaceshipId = res.SpaceshipId,
                SpaceshipVisitDate = res.SpaceshipVisitDate,
                SpaceshipName = res.SpaceshipName,
                HumanId = res.HumanId,
                HumanName = res.HumanName

            });
            return result;
        }
    }
}
