using AutofacSample.Infrastructure.Services.Interfaces;

namespace AutofacSample.Infrastructure.Services;

public class SmsService : ISmsService
{
    public string ReadMessage()
    {
        return "This is a new sms message";
    }
}
