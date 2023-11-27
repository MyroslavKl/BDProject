using AlienProject.GenerateTables;
using AlienProject.Models;

namespace AlienProject.Additional
{
    public class AdditionalExcursion
    {
        private readonly AlienDbContext _context;

        public AdditionalExcursion(AlienDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<ExcursionViewModel> CommonExcursion(string alienName, string humanName, DateTime fromDate, DateTime toDate)
        {
            Generate generate = new(_context);
            var excursions = generate.GenerateExcursionTable();

            var commonExcursions = excursions
                  .Where(excursion => excursion.AlienName == alienName && excursion.HumanName == humanName && excursion.ExcursionDate >= fromDate && excursion.ExcursionDate <= toDate)
                  .ToList();

            return commonExcursions;
        }
    }
}
