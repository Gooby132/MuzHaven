using DomainSeed;
using FluentResults;
using ProjectService.Domain.Errors;
using ProjectService.Domain.ValueObjects;

namespace ProjectService.Domain;

public class Project : Aggregate<Guid>
{

    private readonly Queue<DomainEvent> _domainEvents = new Queue<DomainEvent>();

    public string CreatorId { get; init; }
    public Title Title { get; private set; }
    public string Album { get; private set; }
    public Description Description { get; private set; }
    public DateTime CreatedInUtc { get; init; }
    public DateTime? ReleaseInUtc { get; init; }
    public float BeatsPerMinute { get; private set; }
    public MusicalProfile? MusicalProfile { get; private set; }

    public static Result<Project> Create(string creatorId, string title, string album, string description, string? releaseInUtc, float beatsPerMinute, int? musicalKey, int? musicalScale)
    {

        List<IError> errors = new List<IError>();

        var titleValueObject = Title.Create(title);
        var descriptionValueObject = Description.Create(description);
        var musicalProfile = MusicalProfile.Create(musicalKey, musicalScale);

        if (titleValueObject.IsFailed)
            errors.AddRange(titleValueObject.Errors);

        if (descriptionValueObject.IsFailed)
            errors.AddRange(descriptionValueObject.Errors);

        if (musicalProfile.IsFailed)
            errors.AddRange(musicalProfile.Errors);

        DateTime? releaseInUtcDateTime = null;
        if (!string.IsNullOrEmpty(releaseInUtc))
        {
            if (!DateTime.TryParse(releaseInUtc, out var temp))
                errors.Add(ReleaseDateErrors.CouldNotParseReleaseDate());

            if (temp <
                DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(1))) // compensation for traversal time
                errors.Add(ReleaseDateErrors.ReleaseDateSetToPast());

            releaseInUtcDateTime = temp;
        }

        if (errors.Any())
            return Result.Fail(errors);

        return new Project
        {
            CreatorId = creatorId,
            Title = titleValueObject.Value,
            Album = album,
            Description = descriptionValueObject.Value,
            ReleaseInUtc = releaseInUtcDateTime,
            CreatedInUtc = DateTime.UtcNow,
            BeatsPerMinute = beatsPerMinute,
            MusicalProfile = musicalProfile.Value,
        };
    }

    public override DomainEvent? DequeueDomainEvent() => _domainEvents.Any() ? _domainEvents.Dequeue() : null;
}
