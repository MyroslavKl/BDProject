using System;
using System.Collections.Generic;

namespace AlienProject.Models
{
    public partial class Alien
    {
        public Alien()
        {
            Abductions = new HashSet<Abduction>();
            Excursions = new HashSet<Excursion>();
            Experiments = new HashSet<Experiment>();
            Killings = new HashSet<Killing>();
        }

        public int AlienId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Abduction> Abductions { get; set; }
        public virtual ICollection<Excursion> Excursions { get; set; }
        public virtual ICollection<Experiment> Experiments { get; set; }
        public virtual ICollection<Killing> Killings { get; set; }
    }
}
