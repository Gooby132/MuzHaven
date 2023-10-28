using DomainSeed;
using FluentResults;
using StemService.Domain.Entities;

namespace StemService.Domain;

public class Stem : Aggregate<Guid>
{

    public Guid ProjectId { get; init; }
    public Guid UserId { get; init; }
    public List<Comment> Comments { get; init; } = new List<Comment>();
    public string MediaFile { get; init; }
    public string Name { get; init; }
    public string Instrument { get; init; }

    internal Stem()
    {
        
    }

    public Result CreateComment(Guid userId, string text)
    {
        var comment = Comment.Create(
            userId,
            text
        );

        Comments.Add(comment.Value);

        return Result.Ok();
    }

}
