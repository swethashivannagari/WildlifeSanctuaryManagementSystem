namespace WildlifeSanctuaryManagementSystem.Models
{
    public class AnimalDTO
    {
        public int AnimalId { get; set; }
        public string Species { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string HealthStatus { get; set; }
        public string CurrentLocation { get; set; }
        public DateTime LastCheckupDate { get; set; }
        public int SanctuaryId { get; set; }
        public string SanctuaryName { get; set; }
    }
}
