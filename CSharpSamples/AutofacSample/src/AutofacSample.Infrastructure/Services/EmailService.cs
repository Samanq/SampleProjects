using AutofacSample.Infrastructure.Services.Interfaces;

namespace AutofacSample.Infrastructure.Services;

public class EmailService : IEmailService
{
    public string ReadEmail()
    {
        return "This is a new email";
    }

    public bool SendEmail(string body)
    {
        return true;
    }
}
