using System;
using System.Collections.Generic;

namespace AlienProject.Models
{
    public partial class Abduction
    {
        public int AbductionId { get; set; }
        public DateTime AbductionDate { get; set; }
        public int? HumanId { get; set; }
        public int? AlienId { get; set; }

        public virtual Alien? Alien { get; set; }
        public virtual Human? Human { get; set; }
    }
}
