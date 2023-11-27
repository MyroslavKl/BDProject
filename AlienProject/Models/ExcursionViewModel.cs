namespace AlienProject.Models
{
    public class ExcursionViewModel
    {
        public int ExcursionId { get; set; }
        public DateTime ExcursionDate { get; set; }
        public int? HumanId { get; set; }
        public int? AlienId { get; set; }
        public string AlienName { get; set; }
        public DateTime? AlienBirthDate { get; set; }
        public string AlienEmail { get; set; }
        public string HumanName { get; set; }
        public DateTime? HumanBirthDate { get; set; }
        public string HumanEmail { get; set; }
    }
}
