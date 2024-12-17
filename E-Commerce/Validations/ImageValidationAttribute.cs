using E_Commerce.Dtos;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace E_Commerce.Validations
{
    public class ImageValidationAttribute : ValidationAttribute
    {
        private readonly string[] _allowedExtensions;
        private readonly long _maxFileSize;

        public ImageValidationAttribute(string[] allowedExtensions, long maxFileSize)
        {
            _allowedExtensions = allowedExtensions;
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file == null)
            {
                return ValidationResult.Success; // Not required, allow null if no file is provided
            }

            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!_allowedExtensions.Contains(extension))
            {
                return new ValidationResult($"Invalid file type. Allowed extensions are: {string.Join(", ", _allowedExtensions)}.");
            }

            if (file.Length > _maxFileSize)
            {
                return new ValidationResult($"File size cannot exceed {_maxFileSize / (1024 * 1024)} MB.");
            }

            return ValidationResult.Success;
        }
    }
}
