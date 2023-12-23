using DomainSeed;
using FluentResults;

namespace StemService.Domain.Entities;

public class Comment : Entity<Guid>
{
    public Guid? Parent { get; init; }
    public Guid CommenterId { get; init; }
    public string Text { get; init; }
    public string CreatedOnUtc { get; init; }
    public int? Time { get; init; }

    private Comment() { }

    public static Result<Comment> Create(Guid commenterId, string text, Guid? parent = null, int? time = null)
    {
        return new Comment
        {
            Parent = parent,
            CommenterId = commenterId,
            CreatedOnUtc = DateTime.UtcNow.ToString("O"),
            Text = text,
            Time = time
        };
    }

    internal Result<Comment> BeReplyed(Guid replierId, string text)
    {
        return new Comment
        {
            Parent = Id,
            CommenterId = replierId,
            Text = text,
            CreatedOnUtc = DateTime.UtcNow.ToString("O"),
            Time = null
        };
    }
}
