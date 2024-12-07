using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WildlifeSanctuaryManagementSystem.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }

        [Required(ErrorMessage = "Report type is required.")]
        [StringLength(50, ErrorMessage = "Report type cannot exceed 50 characters.")]
        public string Type { get; set; } // Type of report (e.g., "Incident", "Project Status")

        [Required(ErrorMessage = "Generated date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format for generated date.")]
        public DateTime GeneratedDate { get; set; } // Date when the report was generated

        [Required(ErrorMessage = "Report data is required.")]
        [StringLength(5000, ErrorMessage = "Report data is too large. Maximum 5000 characters allowed.")]
        public string Data { get; set; } // Contents or details of the report

        [Required(ErrorMessage = "Created by information is required.")]
        public int UserId { get; set; } // Foreign key reference to User

        
        //[JsonIgnore]
        //[ValidateNever]
        //public User CreatedBy { get; set; } // Navigation property to User entity
    }
}
