using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? GetByEmail(string email);
    void Add(User user);
}
