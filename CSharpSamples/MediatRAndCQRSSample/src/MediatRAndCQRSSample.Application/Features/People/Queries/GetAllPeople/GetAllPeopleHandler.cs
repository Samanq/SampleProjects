using MediatR;
using MediatRAndCQRSSample.Application.Repositories;
using MediatRAndCQRSSample.Domain.Entities;

namespace MediatRAndCQRSSample.Application.Features.People.Queries.GetAllPeople;

// The query for this handler is: GetAllPeopleQuery
// The return type is: IEnumerable<Person>
public class GetAllPeopleHandler : IRequestHandler<GetAllPeopleQuery, IEnumerable<Person>>
{
    private readonly IPesonRepository _personRepository;

    // Injecting IPersonRepository
    public GetAllPeopleHandler(IPesonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public Task<IEnumerable<Person>> Handle(GetAllPeopleQuery request, CancellationToken cancellationToken)
    {
        // Getting the data from repository
        return Task.FromResult(_personRepository.GetAll());
    }
}
