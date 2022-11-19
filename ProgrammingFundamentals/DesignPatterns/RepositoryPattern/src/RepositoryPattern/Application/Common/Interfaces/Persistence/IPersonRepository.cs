// Our Repository Interface can be in Application layer.
namespace RepositoryPattern.Application.Common.Interfaces.Persistence;
using RepositoryPattern.Domain.Entities;

public interface IPersonRepository
{
    void AddUser(Person person);

    Person? GetPersonById(int id);

    Person? GetPersonByEmail(string email);

    IEnumerable<Person>? GetPeople();

    void Delete(Person person);
}
