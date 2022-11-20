using MediatR;
using MediatRAndCQRSSample.Domain.Entities;

namespace MediatRAndCQRSSample.Application.Features.People.Queries.GetPersonById
{
    public record GetPersonByIdQuery(int id) : IRequest<Person>;
}
