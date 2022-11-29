using ErrorOr;

namespace ErroOrSample.Domian.Common.Errors;

public static class Errors
{
    public static class Students
    {
        public static Error DuplicatedEmail => Error.Conflict(
            code: "Student.DuplicateEmail",
            description: "Email already exist.");
    }
}
