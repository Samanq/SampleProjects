using MoqSample.Infrastructure.Entities;

namespace MoqSample.Infrastructure.Services.Interfaces;
public interface IStudentService
{
    public Student Create(int code, string firstName, string lastName);
}
