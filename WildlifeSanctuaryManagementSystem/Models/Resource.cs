using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using WildlifeSanctuaryManagementSystem.Filters;

namespace WildlifeSanctuaryManagementSystem.Models
{
    public class Resource
    {

            public int ResourceId { get; set; }  // Primary Key

            [Required]
            [StringLength(100, ErrorMessage = "Resource type cannot exceed 100 characters.")]
            public string Type { get; set; }  // Type of resource (e.g., "Animal Feed", "Vet Supplies")

            [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative value.")]
            public int Quantity { get; set; }  // Quantity available

            [StringLength(255, ErrorMessage = "Storage location cannot exceed 255 characters.")]
            public string StorageLocation { get; set; }  // Where the resource is stored (e.g., "Warehouse ")

            [Required]
        [ValidateDateFilter(ErrorMessage = "Last Restocked Date is invalid.")]
        public DateTime LastRestockedDate { get; set; }  // Last time the resource was restocked

            // Foreign Key: Reference to Sanctuary
            public int SanctuaryId { get; set; }

        [JsonIgnore]
        [ValidateNever]
            public Sanctuary Sanctuary { get; set; }
        }
    }

