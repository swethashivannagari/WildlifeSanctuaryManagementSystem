using System.ComponentModel.DataAnnotations;
using System.Data;

namespace WildlifeSanctuaryManagementSystem.Filters
{
    public class ValidateDateFilter:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not DateTime date)
            {
                return new ValidationResult("Invalid date format");
            }
            if (date > DateTime.Now)
            {
                return new ValidationResult("The date cannot be in the future.");
            }
            return ValidationResult.Success;
        }
    }
}
