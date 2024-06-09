using DomainSeed;

namespace StemService.Domain.Entities.Comments.ValueObjects;

public class Commenter : ValueObject
{

    public Guid Id { get; init; }
    public string? StageName { get; init; }

    private Commenter() { }

    public static Commenter Create(Guid commenterId, string stageName)
    {
        return new Commenter
        {
            Id = commenterId,
            StageName = stageName
        };
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Id;
        yield return StageName;
    }
}
