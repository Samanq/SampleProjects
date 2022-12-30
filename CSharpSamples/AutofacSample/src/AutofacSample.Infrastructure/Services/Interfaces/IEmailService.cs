namespace AutofacSample.Infrastructure.Services.Interfaces;

public interface IEmailService
{
    string ReadEmail();
    bool SendEmail(string body);
}
