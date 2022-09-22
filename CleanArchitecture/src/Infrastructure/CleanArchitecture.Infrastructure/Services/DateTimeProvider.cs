namespace CleanArchitecture.Infrastructure.Services;
using CleanArchitecture.Application.Common.Interfaces.Services;
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.UtcNow;
}
