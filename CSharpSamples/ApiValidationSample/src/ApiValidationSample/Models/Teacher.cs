using System.ComponentModel.DataAnnotations;

namespace ApiValidationSample.Models
{
    public class Teacher : IValidatableObject
    {
        public string Name { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (BirthDate.Year < 2015)
            {
                yield return new ValidationResult("Date is incorrect");
            }
        }
    }
}
