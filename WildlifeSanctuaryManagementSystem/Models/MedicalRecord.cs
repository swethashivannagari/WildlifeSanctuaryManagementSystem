using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WildlifeSanctuaryManagementSystem.Models
{
    public class MedicalRecord
    {
        [Key]
        public int RecordId {  get; set; }

        [Required(ErrorMessage ="Animal Id is required.")]
        public int AnimalId {  get; set; }

        [Required(ErrorMessage ="Date is required.")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Diagnosis is required.")]
        [StringLength(200,ErrorMessage ="Diagnosis should not exceed 200 characters.")]
        public string Diagnosis { get; set; }

        [Required(ErrorMessage ="Treatment Details are Required.")]
        [StringLength(500,ErrorMessage ="Treatment should not exceed 500 characters.")]
        public string Treatment {  get; set; }

        [Required(ErrorMessage ="Vet id is required.")]
        public int VetId {  get; set; }

        [DataType(DataType.Date)]
        public DateTime? NextCheckup {  get; set; }

        [JsonIgnore]
        [ValidateNever]
        public Animal Animal { get; set; }

        //[JsonIgnore]
        //[ValidateNever]
        //[ForeignKey("VetId")]
        //public User Veterian { get; set; }

    }
}
