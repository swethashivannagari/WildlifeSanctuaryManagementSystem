using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WildlifeSanctuaryManagementSystem.Filters;

namespace WildlifeSanctuaryManagementSystem.Models
{
    public class Incident
    {
        public int IncidentId { get; set; } 

        [Required(ErrorMessage = "Sanctuary ID is required.")]
        public int SanctuaryId { get; set; }  // Foreign Key to Sanctuary

        [Required(ErrorMessage = "Date is required.")]
        [ValidateDateFilter( ErrorMessage = "Invalid date format for Incident Date.")]
        
        public DateTime Date { get; set; }  // Date of the incident

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }  // Description of the incident (e.g., "Poaching attempt")

        [Required(ErrorMessage = "Severity is required.")]
        [RegularExpression("^(Low|Medium|High|Critical)$", ErrorMessage = "Severity must be 'Low', 'Medium', 'High', or 'Critical'.")]
        public string Severity { get; set; }  // Severity level (Low, Medium, High, Critical)

        [Required(ErrorMessage = "Resolution Status is required.")]
       
        [RegularExpression("^(Resolved|Unresolved|In Progress|Closed)$", ErrorMessage = "Resolution Status must be 'Resolved', 'Unresolved', 'In Progress', or 'Closed'.")]
        public string ResolutionStatus { get; set; }  // Resolution status of the incident (e.g., Resolved, Unresolved)

        [Required(ErrorMessage = "Reported By is required.")]
        public int ReportedById { get; set; }  // Foreign Key to User who reported the incident

        [JsonIgnore]
       public Sanctuary? Sanctuary { get; set; }
       // public User? ReportedBy{ get; set; }

    }
}
