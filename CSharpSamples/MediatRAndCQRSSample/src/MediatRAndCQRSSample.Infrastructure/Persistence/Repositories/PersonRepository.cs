using MediatRAndCQRSSample.Application.Repositories;
using MediatRAndCQRSSample.Domain.Entities;

namespace MediatRAndCQRSSample.Infrastructure.Persistence.Repositories;

public class PersonRepository : IPesonRepository
{
    private static List<Person> _people = new List<Person> 
    {
        new Person
        {
            Id = 1,
            Name = "John",
            Email = "John@Example.com",
        },
        new Person 
        {
            Id = 2,
            Name = "Jane",
            Email = "Jane@Example.com",
        }
    };

    public Person Create(Person person)
    {
        _people.Add(person);
        return person;
    }

    public IEnumerable<Person>? GetAll()
    {
        return _people;
    }

    public Person? GetByEmail(string email)
    {
        return _people
            .Where(p => p.Email.ToLower() == email.ToLower())
            .SingleOrDefault();
    }

    public Person? GetById(int id)
    {
        return _people
            .Where(p => p.Id == id)
            .SingleOrDefault();
    }
}
