using MediatR;

namespace DomainSeed;

public class DomainEvent : INotification
{

    public DateTime CreatedOnUtc { get; }

    public DomainEvent()
    {
        CreatedOnUtc = DateTime.UtcNow;
    }

}
