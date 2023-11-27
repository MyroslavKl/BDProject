using System;
using System.Collections.Generic;

namespace AlienProject.Models
{
    public partial class SpaceshippVisit
    {
        public int SpaceshipVisitId { get; set; }
        public int? SpaceshipId { get; set; }
        public DateTime SpaceshipVisitDate { get; set; }
        public int? HumanId { get; set; }

        public virtual Human? Human { get; set; }
        public virtual Spaceshipp? Spaceship { get; set; }
    }
}
