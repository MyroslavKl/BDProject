using AlienProject.GenerateTables;
using AlienProject.Models;

namespace AlienProject.Additional
{
    public class AdditionalExperiment
    {
        private readonly AlienDbContext _context;

        public AdditionalExperiment(AlienDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<ExperimentViewModel> CommonExperiment(string alienName,string humanName, DateTime fromDate, DateTime toDate) {
            Generate generate = new(_context);
            var experiments = generate.GenerateExperimentsTable();
            
             var commonExperiments = experiments
                   .Where(experiment => experiment.AlienName == alienName && experiment.HumanName == humanName && experiment.ExperimentDate >= fromDate && experiment.ExperimentDate <= toDate)
                   .ToList();

            return commonExperiments;
        }
    }
}
