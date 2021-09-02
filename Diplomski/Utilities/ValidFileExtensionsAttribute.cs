using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Diplomski.Utilities
{
    public class ValidFileExtensionsAttribute : ValidationAttribute
    {
        private readonly string _allowedFileExtension; // Coma delimited file extensions

        public ValidFileExtensionsAttribute(string allowedFileExtensions) 
        {
            _allowedFileExtension = allowedFileExtensions;
        }

        private string GetErrorMessage() => $"Dozvoljeni su samo fajlovi sa ekstenzijama {_allowedFileExtension}";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is null)
            {
                return ValidationResult.Success;
            }

            var file = (IFormFile)value;
            var fileExtension = Path.GetExtension(file.FileName);

            if(_allowedFileExtension.ToLower().Contains(fileExtension))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(GetErrorMessage());
        }
    }
}
