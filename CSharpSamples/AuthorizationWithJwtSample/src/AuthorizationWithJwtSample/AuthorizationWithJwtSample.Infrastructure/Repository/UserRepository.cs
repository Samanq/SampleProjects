using AuthorizationWithJwtSample.Application.Repositories;
using AuthorizationWithJwtSample.Domain.Entities;

namespace AuthorizationWithJwtSample.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private static List<User> _users = new List<User>();
    

    public User? Create(
        string name,
        string email,
        byte[] passwordHash,
        byte[] passwordSalt,
        string refreshToken,
        DateTime refreshTokenExpiryDateTime)
    {
        var user = new User
        {
            Id = 2,
            Name = name,
            Email = email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            RefreshToken = refreshToken,
            RefreshTokenExpiryDate = refreshTokenExpiryDateTime
        };
        _users.Add(user);

        return user;
    }

    public IEnumerable<User>? GetAll()
    {
        return _users;
    }

    public User? GetByEmail(string email)
    {
        return _users
            .Where(u => u.Email.ToLower() == email.ToLower())
            .SingleOrDefault();
    }

    public User? GetById(int id)
    {
        return _users
            .Where(u => u.Id == id)
            .SingleOrDefault();
    }
}
