using MoqSample.Infrastructure.Entities;
using MoqSample.Infrastructure.Services.Interfaces;

namespace MoqSample.Infrastructure.Services;

public class StudentService : IStudentService
{
    private readonly ICodeValidator _codeValidator;

    public string Status { get; set; }

    public StudentService(ICodeValidator codeValidator)
    {
        _codeValidator = codeValidator;
        Status = "Active";
    }

    public Student Create(int code, string firstName, string lastName)
    {

        if (_codeValidator.IsValid(code))
        {
            return new Student
            {
                Code = code,
                FirstName = firstName,
                LastName = lastName
            };

        }
        throw new ArgumentOutOfRangeException();
    }
}
