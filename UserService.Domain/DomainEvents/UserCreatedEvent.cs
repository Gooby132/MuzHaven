using DomainSeed;

namespace UserService.Domain.DomainEvents;

public class UserCreatedEvent : DomainEvent
{

    public Guid UserId { get; }

    public UserCreatedEvent(Guid userId)
    {
        UserId = userId;
    }

}
