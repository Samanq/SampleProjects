using ApiValidationSample.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ApiValidationSample.Models
{
    public class Student
    {
        [Required(AllowEmptyStrings = true)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(10)] // Max 10 Charecters
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Range(10,99)] // Number between 10 to 99
        public int Age { get; set; }

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage ="Code should contain only a-z letters")]
        public string Code { get; set; } = string.Empty;

        [StudentTypeValidation]
        public string Type { get; set; } = string.Empty;
    }
}
