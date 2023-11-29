namespace AlienProject.Additional
{
    public class ExcursionInfo
    {
        public int ExcursionId { get; set; }
        public DateTime ExcursionDate { get; set; }
        public int? HumanId { get; set; }
        public int? AlienId { get; set; }
        public string AlienName { get; set; }
        public string HumanName { get; set; }
        public int PeopleCount { get; set; }
    }
}
