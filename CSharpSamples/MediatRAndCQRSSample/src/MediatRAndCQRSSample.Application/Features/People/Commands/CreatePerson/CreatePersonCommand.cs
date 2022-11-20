using MediatR;
using MediatRAndCQRSSample.Domain.Entities;

namespace MediatRAndCQRSSample.Application.Features.People.Commands.CreatePerson;

public record CreatePersonCommand(
    int Id,
    string Name,
    string Email) : IRequest<Person>;
