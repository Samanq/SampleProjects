// Impementation of Repository can be in Infrastructure layer.
namespace RepositoryPattern.Infrastructure.Persistence.Repositories;

using RepositoryPattern.Application.Common.Interfaces.Persistence;
using RepositoryPattern.Domain.Entities;

public class PersonRepository : IPersonRepository
{
    private static List<Person> _persons = new List<Person>();

    public void AddUser(Person person)
    {
        _persons.Add(person);
    }

    public void Delete(Person person)
    {
        _persons.Remove(person);
    }

    public IEnumerable<Person>? GetPeople()
    {
        return _persons;
    }

    public Person? GetPersonByEmail(string email)
    {
        return _persons.Where(p => p.Email.ToLower() == email.ToLower())
            .FirstOrDefault();
    }

    public Person? GetPersonById(int id)
    {
        return _persons.Where(p => p.Id == id)
            .FirstOrDefault();
    }
}
