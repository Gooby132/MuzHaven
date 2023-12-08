using DomainSeed;
using FluentResults;
using ProjectService.Domain;
using UserService.Domain.DomainEvents;
using UserService.Domain.ValueObjects;

namespace UserService.Domain;

public class User : Aggregate<Guid>
{

    private readonly Queue<DomainEvent> _domainEvents = new Queue<DomainEvent>();

    public PersonMetaData MetaData { get; init; }
    public ArtistDescription ArtistDescription { get; init; }

    private User() { }

    private User(PersonMetaData metaData, ArtistDescription artistDescription)
    {
        MetaData = metaData;
        ArtistDescription = artistDescription;
    }

    public Result<Project> CreateNewProject(
        string title,
        string album,
        string description,
        string? releaseInUtc,
        float beatsPerMinute,
        int? musicalKey,
        int? musicalScale)
    {
        var project = Project.Create(
                Id.ToString(),
                title, 
                album, 
                description,
                releaseInUtc,
                beatsPerMinute,
                musicalKey,
                musicalScale
            );

        return project;
    }

    public static Result<User> Create(PersonMetaData metaData, ArtistDescription artistDescription)
    {
        var user = new User(metaData, artistDescription);

        user._domainEvents.Enqueue(new UserCreatedEvent(user.Id));

        return user;
    }

    public static Result<User> Login(string? id, PersonMetaData metaData, ArtistDescription artistDescription)
    {
        if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out var guid))
            return Result.Fail("user id was invalid");

        var user = new User(metaData, artistDescription)
        {
            Id = guid
        };

        return user;
    }

    public override DomainEvent? DequeueDomainEvent() => _domainEvents.Any() ? _domainEvents.Dequeue() : null;

}
