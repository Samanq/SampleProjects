using MediatRAndCQRSSample.Domain.Entities;

namespace MediatRAndCQRSSample.Application.Repositories;

public interface IPesonRepository
{
    IEnumerable<Person>? GetAll();

    Person? GetById(int id);

    Person? GetByEmail(string email);

    Person Create(Person person);
}
