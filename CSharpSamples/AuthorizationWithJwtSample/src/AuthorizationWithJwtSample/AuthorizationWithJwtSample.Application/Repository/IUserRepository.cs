using AuthorizationWithJwtSample.Domain.Entities;

namespace AuthorizationWithJwtSample.Application.Repository;

public interface IUserRepository
{
    User? GetById(int id);
    User? GetByEmail(string email);
    IEnumerable<User> GetAll();
    User? Create(
        string name,
        string email,
        byte[] passwordHash,
        byte[] passwordSalt,
        string refreshToken,
        DateTime refreshTokenExpiryDateTime);
}
