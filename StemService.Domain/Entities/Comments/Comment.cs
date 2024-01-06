using DomainSeed;
using FluentResults;
using StemService.Domain.Entities.Comments.ValueObjects;

namespace StemService.Domain.Entities.Comments;

public class Comment : Entity<Guid>
{
    public Guid? Parent { get; init; }
    public Commenter Commenter { get; init; }
    public string Text { get; init; }
    public DateTime CreatedOnUtc { get; init; }
    public int? Time { get; init; }

    private Comment() { }

    public static Result<Comment> Create(Guid commenterId, string text, string stageName, Guid? parent = null, int? time = null)
    {
        return new Comment
        {
            Parent = parent,
            Commenter = Commenter.Create(commenterId, null, null, null),
            CreatedOnUtc = DateTime.UtcNow,
            Text = text,
            Time = time
        };
    }

    internal Result<Comment> BeReplyed(Guid replierId, string text)
    {
        return new Comment
        {
            Parent = Id,
            Commenter = Commenter.Create(replierId, null, null,null),
            Text = text,
            CreatedOnUtc = DateTime.UtcNow,
            Time = null
        };
    }
}
