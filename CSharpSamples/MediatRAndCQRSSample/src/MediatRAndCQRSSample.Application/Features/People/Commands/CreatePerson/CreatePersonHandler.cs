using MediatR;
using MediatRAndCQRSSample.Application.Repositories;
using MediatRAndCQRSSample.Domain.Entities;

namespace MediatRAndCQRSSample.Application.Features.People.Commands.CreatePerson
{
    public class CreatePersonHandler : IRequestHandler<CreatePersonCommand, Person>
    {
        private readonly IPesonRepository _pesonRepository;

        public CreatePersonHandler(IPesonRepository pesonRepository)
        {
            _pesonRepository = pesonRepository;
        }

        public Task<Person> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var result = _pesonRepository.Create(new Person
            {
                Id = request.Id, 
                Name = request.Name,
                Email = request.Email
            });

            return Task.FromResult(result);
        }
    }
}
