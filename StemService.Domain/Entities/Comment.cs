using DomainSeed;
using FluentResults;

namespace StemService.Domain.Entities;

public class Comment : Entity<Guid>
{
    public Guid StemId { get; set; }
    public Guid CommenterId { get; init; }
    public string Text { get; init; }
    public int? Time { get; set; }

    private Comment() { }

    public static Result<Comment> Create(Guid userId, string text, int? time)
    {
        return new Comment
        {
            CommenterId = userId,
            Text = text,
            Time = time
        };
    }

}
