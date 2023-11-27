using System;
using System.Collections.Generic;

namespace AlienProject.Models
{
    public partial class AlienExcursion
    {
        public int? ExcursionId { get; set; }
        public int? AlienId { get; set; }

        public virtual Alien? Alien { get; set; }
        public virtual Excursion? Excursion { get; set; }
    }
}
