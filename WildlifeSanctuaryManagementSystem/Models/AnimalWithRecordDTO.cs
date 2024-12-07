namespace WildlifeSanctuaryManagementSystem.Models
{
    public class AnimalWithRecordDTO
    {
        public int AnimalId { get; set; }
        public string Species { get; set; }

        public int Age { get; set; }
        public string? ReportDetails { get; set; } 
        public DateTime? ReportDate { get; set; }
    }
}
