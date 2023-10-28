using DomainSeed;
using FluentResults;

namespace StemService.Domain.Entities;

public class Comment : Entity<Guid>
{

    public Guid UserId { get; init; }
    public string Text { get; init; }

    private Comment()
    {
    }

    public static Result<Comment> Create(Guid userId, string text)
    {
        return new Comment
        {
            UserId = userId,
            Text = text,
        };
    }

}
