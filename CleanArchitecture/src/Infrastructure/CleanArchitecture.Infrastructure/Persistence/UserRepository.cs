namespace CleanArchitecture.Infrastructure.Persistence;

using CleanArchitecture.Application.Common.Interfaces.Persistence;
using CleanArchitecture.Domain.Entities;

public class UserRepository : IUserRepository
{
    private static List<User> _users = new();

    public void Add(User user)
    {
        _users.Add(user);
    }

    public User? GetByEmail(string email)
    {
        return _users.FirstOrDefault(t => t.Email == email);
    }
}
