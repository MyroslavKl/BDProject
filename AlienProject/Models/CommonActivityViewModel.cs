namespace AlienProject.Models
{
    public class CommonActivityViewModel
    {
        public int ActivityId { get; set; }
        public DateTime ActivityDate { get; set; }
        public string AlienName { get; set; }
        public DateTime? AlienBirthDate { get; set; }
        public string AlienEmail { get; set; }
        public string HumanName { get; set; }
        public DateTime? HumanBirthDate { get; set; }
        public string HumanEmail { get; set; }
        public string Type { get; set; }
    }
}
