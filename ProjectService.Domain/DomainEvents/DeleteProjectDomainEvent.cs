using DomainSeed;
using MediatR;

namespace ProjectService.Domain.DomainEvents;

public class DeleteProjectDomainEvent : DomainEvent
{
    public Project Project { get; init; } = default!;
}
