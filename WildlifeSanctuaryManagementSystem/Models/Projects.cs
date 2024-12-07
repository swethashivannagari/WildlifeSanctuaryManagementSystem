using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WildlifeSanctuaryManagementSystem.Models
{
    public class Project
    {
        public int ProjectId { get; set; } // Primary Key

        [Required(ErrorMessage = "Sanctuary ID is required.")]
        public int SanctuaryId { get; set; }  // Foreign Key to Sanctuary

        [Required(ErrorMessage = "Activity Type is required.")]
        [StringLength(100, ErrorMessage = "Activity Type cannot exceed 100 characters.")]
        public string ActivityType { get; set; }  

        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format for Start Date.")]
        public DateTime StartDate { get; set; } 

        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format for End Date.")]
       
        public DateTime EndDate { get; set; }  // End date of the project

        [Required(ErrorMessage = "Assigned Ranger is required.")]
        public int RangerId { get; set; }  // Foreign Key to User (representing the assigned ranger)

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        [RegularExpression("^(Active|Completed|Pending|Cancelled)$", ErrorMessage = "Status must be 'Active', 'Completed', 'Pending', or 'Cancelled'.")]
        public string Status { get; set; }  // Status of the project (e.g., "Active", "Completed")

        //[JsonIgnore]
        //[ValidateNever]
        //public User AssignedRanger { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public Sanctuary Sanctuary { get; set; }
    }
}
