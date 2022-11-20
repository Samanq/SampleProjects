using MediatR;
using MediatRAndCQRSSample.Domain.Entities;

namespace MediatRAndCQRSSample.Application.Features.People.Queries.GetAllPeople;

// The return type is: IEnumerable<Person>
public record GetAllPeopleQuery() : IRequest<IEnumerable<Person>>;
