using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

namespace WildlifeSanctuaryManagementSystem.Models
{
    public class WildlifeData
    {
        [Key]
        public int DataId { get; set; }

        [Required]
        public int SanctuaryId { get; set; } 

        [ForeignKey("SanctuaryId")]
        [JsonIgnore]
        [ValidateNever]
        public virtual Sanctuary Sanctuary { get; set; } 

        [Required]
        [StringLength(100)]
        public string Species { get; set; }

        [Required]
        public DateTime ObservationDate { get; set; }

        [StringLength(500)]
        public string BehavioralReport { get; set; }

        public int? PopulationEstimate { get; set; }

        [Required]
        
        public int BiologistId { get; set; }

        //[JsonIgnore]
        //[ValidateNever]
        //[ForeignKey("BiologistId")]
        //public virtual User Biologist { get; set; } 
    }
}
