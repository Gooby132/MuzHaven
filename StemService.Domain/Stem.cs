using DomainSeed;
using FluentResults;
using StemService.Domain.Entities;
using StemService.Domain.ValueObjects;

namespace StemService.Domain;

public class Stem : Aggregate<Guid>
{

    public Guid ProjectId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; internal set; }
    public List<Comment> Comments { get; init; } = new List<Comment>();
    public Description Desciption { get; init; }
    public MusicFile? MusicFile { get; internal set; } // should be considered as private soon
    public string Instrument { get; init; }

    internal Stem() { }

    public Result CreateComment(Guid userId, string text, int? time)
    {
        var comment = Comment.Create(
            userId,
            text,
            time
        );

        Comments.Add(comment.Value);

        return Result.Ok();
    }

    internal Result InitializeMusicFile(MusicFile musicFile)
    {
        if(MusicFile is null)
            return Result.Fail("Cannot initialize file twice"); // not typed error 

        MusicFile = musicFile;

        return Result.Ok();
    }

    public override DomainEvent? DequeueDomainEvent()
    {
        return null;
    }
}
