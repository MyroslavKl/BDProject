using System;
using System.Collections.Generic;

namespace AlienProject.Models
{
    public partial class AlienExperiment
    {
        public int? ExperimentId { get; set; }
        public int? AlienId { get; set; }

        public virtual Alien? Alien { get; set; }
        public virtual Experiment? Experiment { get; set; }
    }
}
