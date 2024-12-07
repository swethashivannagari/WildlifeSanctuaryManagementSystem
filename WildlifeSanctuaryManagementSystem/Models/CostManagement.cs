using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WildlifeSanctuaryManagementSystem.Models
{
    public class CostManagement
    {
        // Primary key for the CostManagement table
        [Key]
        public int CostId { get; set; }

        // Foreign key for the Sanctuary table
        [Required(ErrorMessage = "Sanctuary ID is required.")]
        public int SanctuaryId { get; set; }

        // Reference to the Sanctuary object for navigation
        [JsonIgnore]
        [ValidateNever]
        public Sanctuary Sanctuary { get; set; }

        // Expense type, which cannot exceed 100 characters
        [Required(ErrorMessage = "Expense type is required.")]
        [StringLength(100, ErrorMessage = "Expense type cannot exceed 100 characters.")]
        public string ExpenseType { get; set; }

        // Amount must be a positive value
        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be a positive value.")]
        public decimal Amount { get; set; }

        // Date field, which should have a valid date format
        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime Date { get; set; }

        // Foreign key for the User (Responsible person)
        [Required(ErrorMessage = "Responsible person is required.")]
        //[ForeignKey("UserId")]
        public int ResponsiblePersonId { get; set; }

        // Reference to the User object for navigation (Responsible Person)
        //[JsonIgnore]
        //[ValidateNever]
        //public User ResponsiblePerson { get; set; }
    }
}
