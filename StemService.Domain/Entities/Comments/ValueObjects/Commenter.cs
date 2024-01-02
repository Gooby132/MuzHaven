using DomainSeed;

namespace StemService.Domain.Entities.Comments.ValueObjects;

public class Commenter : ValueObject
{

    public Guid Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? StageName { get; init; }

    private Commenter() { }

    public static Commenter Create(Guid commenterId, string firstName, string lastName, string stageName)
    {
        return new Commenter
        {
            Id = commenterId,
            FirstName = firstName,
            LastName = lastName,
            StageName = stageName
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
