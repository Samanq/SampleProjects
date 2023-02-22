using MoqSample.Infrastructure.Services.Interfaces;

namespace MoqSample.Infrastructure.Services;

internal class CodeValidator : ICodeValidator
{
    public string Status { get; set; } = string.Empty;

    public bool IsValid(int code)
    {
        return code > 10 && code < 20;
    }

    public void IsValidWithOut(int code, out bool isCodeValid)
    {
        isCodeValid = code > 10 && code < 20;
    }

    public void CheckStatus()
    {
        var currentStatus = Status;
    }

    public void UpdateStatus()
    {
        Status = "Updated Status";
    }
}
