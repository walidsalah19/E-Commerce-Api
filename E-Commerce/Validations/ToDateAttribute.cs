using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Validations
{
    public class ToDateAttribute : ValidationAttribute
    {
        public string fromDate { get; set; }

        public ToDateAttribute(string fromDate)
        {
            this.fromDate = fromDate;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = value as DateTime?;

            if (value is DateTime dateTime)
            {
                var fromDateInfo = validationContext.ObjectType.GetProperty(fromDate);
                if (fromDateInfo == null)
                {
                    return new ValidationResult($"Property '{fromDate}' not found.");
                }
                var otherValue = fromDateInfo.GetValue(validationContext.ObjectInstance) as DateTime?;

                var comparisonResult = DateTime.Compare(currentValue.Value, otherValue.Value);

                if (comparisonResult <= 0)
                {
                    return new ValidationResult($"'{validationContext.DisplayName}' must be greater than '{fromDate}'.");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("Invalid date format.");
        }
    }
}
