using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WildlifeSanctuaryManagementSystem.Models
{
    public class User
    {
        [Key]
        public int UserId {  get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [RegularExpression("^(Admin|Manager|Ranger|Biologist|Veterinarian|Conservationist)$",
        ErrorMessage = "Role must be one of the predefined roles")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        public bool IsActive { get; set; } = true;

        //[JsonIgnore]
        //[ValidateNever]
        //public ICollection<Report>? Reports { get; set; }
    }
}
