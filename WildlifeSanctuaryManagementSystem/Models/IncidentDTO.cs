namespace WildlifeSanctuaryManagementSystem.Models
{
    public class IncidentDto
    {
        public int IncidentId { get; set; }
        public int SanctuaryId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Severity { get; set; }
        public string ResolutionStatus { get; set; }
        public int ReportedById { get; set; }

        public string SanctuaryName { get; set; }
    }
}
