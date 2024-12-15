using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Helpers
{
    public class FromDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateTime)
            {
                if (dateTime > DateTime.Now)
                    return ValidationResult.Success;

                return new ValidationResult("Date must be in the future.");
            }

            return new ValidationResult("Invalid date format.");
        }
    }
}
