using FluentResults;

namespace FluentResultSample.Application.Common.Errors;

public class DuplicateEmailError : IError
{
    public List<IError> Reasons => throw new NotImplementedException();

    public string Message => "Email already exist";

    public Dictionary<string, object> Metadata => throw new NotImplementedException();
}
