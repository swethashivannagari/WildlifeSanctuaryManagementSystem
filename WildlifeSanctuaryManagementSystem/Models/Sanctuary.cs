using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WildlifeSanctuaryManagementSystem.Models
{
    public class Sanctuary
    {
        [Key]
        public int SanctuaryId { get; set; }

        [Required(ErrorMessage = "Sanctuary name is required.")]
        [StringLength(100, ErrorMessage = "Sanctuary name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Total area is required.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Total area must be greater than 0.")]
        public double TotalArea { get; set; }

        [Required(ErrorMessage = "Habitat type is required.")]
        public string HabitatType { get; set; }

        public string ProtectedSpecies { get; set; }

        
        public string Status { get; set; } = "Active";

        public int ManagerId { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public User Manager { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public ICollection<Animal>? Animals { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public ICollection<Resource>? Resources { get; set; }
        //public ICollection<Project>? Projects { get; set; }

        //public ICollection<CostManagement>? CostManagements { get; set; }

        //public ICollection<EnvironmentalData>? EnvironmetalData { get; set; }
        //public ICollection<WildlifeData>? WildlifeData { get; set; }



    }
}
