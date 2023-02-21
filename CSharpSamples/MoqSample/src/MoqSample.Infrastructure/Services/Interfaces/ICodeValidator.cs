namespace MoqSample.Infrastructure.Services.Interfaces;

public interface ICodeValidator
{
    bool IsValid(int code);
    public void IsValidWithOut(int code, out bool isCodeValid);
}
