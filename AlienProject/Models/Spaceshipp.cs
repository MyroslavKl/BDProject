using System;
using System.Collections.Generic;

namespace AlienProject.Models
{
    public partial class Spaceshipp
    {
        public Spaceshipp()
        {
            SpaceshippVisits = new HashSet<SpaceshippVisit>();
        }

        public int SpaceshipId { get; set; }
        public string SpaceshipName { get; set; } = null!;

        public virtual ICollection<SpaceshippVisit> SpaceshippVisits { get; set; }
    }
}
