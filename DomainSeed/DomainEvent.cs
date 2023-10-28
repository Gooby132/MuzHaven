using MediatR;

namespace DomainSeed;

public abstract class DomainEvent : INotification
{

    public Guid Id { get; init; }

    public DateTime CreatedOnUtc { get; init; } = DateTime.UtcNow;

    public DomainEvent()
    {
    }

}
