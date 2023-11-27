namespace AlienProject.Models
{
    public class SpaceshipViewModel
    {
        public int SpaceshipVisitId { get; set; }
        public int? SpaceshipId { get; set; }
        public DateTime SpaceshipVisitDate { get; set; }
        public int? HumanId { get; set; }
        public string HumanName { get; set; }
        public string SpaceshipName { get; set; }
    }
}
