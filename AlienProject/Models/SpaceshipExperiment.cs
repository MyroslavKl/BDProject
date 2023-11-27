using System;
using System.Collections.Generic;

namespace AlienProject.Models
{
    public partial class SpaceshipExperiment
    {
        public int? ExperimentId { get; set; }
        public int? SpaceshipId { get; set; }

        public virtual Experiment? Experiment { get; set; }
        public virtual Spaceshipp? Spaceship { get; set; }
    }
}
