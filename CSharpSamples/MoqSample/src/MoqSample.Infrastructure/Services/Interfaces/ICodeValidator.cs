namespace MoqSample.Infrastructure.Services.Interfaces;

public interface ICodeValidator
{
    public string Status { get; set; }
    bool IsValid(int code);
    public void CheckStatus();
    public void UpdateStatus();

    public void IsValidWithOut(int code, out bool isCodeValid);
}
