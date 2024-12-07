using System.ComponentModel.DataAnnotations;
using WildlifeSanctuaryManagementSystem.Filters;

namespace WildlifeSanctuaryManagementSystem.Models
{
    public class CreateAnimalDTO
    {
        [Required(ErrorMessage = "Species is required.")]
        [StringLength(100, ErrorMessage = "Species name cannot exceed 100 characters.")]
        public string Species { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Age must be a non-negative value.")]
        public int Age { get; set; }

        [Required]
        [RegularExpression("^(Male|Female|Unknown)$", ErrorMessage = "Gender must be 'Male', 'Female', or 'Unknown'.")]
        public string Gender { get; set; } = "Unknown";

        [Required]
        [RegularExpression("^(Healthy|Injured|Sick|Critical)$", ErrorMessage = "Health status must be 'Healthy', 'Injured', 'Sick', or 'Critical'.")]
        public string HealthStatus { get; set; }

        [Required(ErrorMessage = "Current location is required.")]
        [StringLength(255, ErrorMessage = "Location cannot exceed 255 characters.")]
        public string CurrentLocation { get; set; }

        [Required]
        [ValidateDateFilter(ErrorMessage = "Last Checkup Date is invalid.")]

        public DateTime LastCheckupDate { get; set; }

        [Required]
        public int SanctuaryId { get; set; }
    }
}
