using DomainSeed;
using FluentResults;
using ProjectService.Domain.ValueObjects;
using System.Runtime.Intrinsics.Arm;

namespace ProjectService.Domain;

public class Project : Aggregate<Guid>
{

    private readonly Queue<DomainEvent> _domainEvents = new Queue<DomainEvent>();

    public MetaData MetaData { get; init; }

    public static Result<Project> Create(MetaData metaData)
    {
        return new Project
        {
            MetaData = metaData,
        };
    }

    public override DomainEvent? DequeueDomainEvent() => _domainEvents.Any() ? _domainEvents.Dequeue() : null;
}
