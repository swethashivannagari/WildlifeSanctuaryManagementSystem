using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WildlifeSanctuaryManagementSystem.Models
{
    public class EnvironmentalData
    {
        [Key]
        public int AssessmentId { get; set; }

        [Required]
        public int SanctuaryId { get; set; }

        [JsonIgnore]
        [ValidateNever]
        [ForeignKey("SanctuaryId")]
        public virtual Sanctuary Sanctuary { get; set; }

        [Required]
        [StringLength(50)]
        public string ImpactType { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [StringLength(500)]
        public string Recommendations { get; set; }

        [Required]
        public int ConductedBy { get; set; }

    }
}
