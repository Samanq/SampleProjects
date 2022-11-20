using MediatR;
using MediatRAndCQRSSample.Application.Repositories;
using MediatRAndCQRSSample.Domain.Entities;

namespace MediatRAndCQRSSample.Application.Features.People.Queries.GetPersonById;

public class GetPersonByIdHandler : IRequestHandler<GetPersonByIdQuery, Person>
{
    private readonly IPesonRepository _personRepository;

    // Injecting Repository
    public GetPersonByIdHandler(IPesonRepository pesonRepository)
    {
        _personRepository = pesonRepository;
    }

    public Task<Person> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        var result = _personRepository.GetById(request.id);
        return Task.FromResult(result);
    }
}
