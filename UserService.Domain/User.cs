using DomainSeed;
using FluentResults;
using UserService.Domain.DomainEvents;
using UserService.Domain.ValueObjects;

namespace UserService.Domain;

public class User : Aggregate<Guid>
{

    private readonly Queue<DomainEvent> _domainEvents = new Queue<DomainEvent>();

    public PersonMetaData MetaData { get; init; }
    public ArtistDescription ArtistDescription { get; init; }
    public string Password { get; init; }

    private User() { }

    private User(PersonMetaData metaData, ArtistDescription artistDescription, string password)
    {
        MetaData = metaData;
        ArtistDescription = artistDescription;
        Password = password;
    }

    public static Result<User> Create(PersonMetaData metaData, ArtistDescription artistDescription, string password)
    {
        var user = new User(metaData, artistDescription, password);

        user._domainEvents.Enqueue(new UserCreatedEvent(user.Id));

        return user;
    }

    public override DomainEvent? DequeueDomainEvent() => _domainEvents.Any() ? _domainEvents.Dequeue() : null;

}
