using System;
using System.Collections.Generic;

namespace AlienProject.Models
{
    public partial class Killing
    {
        public int KillingId { get; set; }
        public DateTime KillingDate { get; set; }
        public int? HumanId { get; set; }
        public int? AlienId { get; set; }

        public virtual Alien? Alien { get; set; }
        public virtual Human? Human { get; set; }
    }
}
