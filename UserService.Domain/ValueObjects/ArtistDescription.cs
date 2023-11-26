using DomainSeed;
using FluentResults;
using UserService.Domain.Errors;

namespace UserService.Domain.ValueObjects;

public class ArtistDescription : ValueObject
{

    public const int MinBioLength = 2;
    public const int MaxBioLength = 150;

    public const int MinStageNameLength = 2;
    public const int MaxStageNameLength = 20;

    public string StageName { get; init; }
    public string Bio { get; init; }

    private ArtistDescription() { }

    public static Result<ArtistDescription> Create(string stageName, string bio)
    {
        List<Error> errors = new List<Error>();

        if (bio.Length < MinBioLength)
            errors.Add(BioError.TooShortCode());

        if (bio.Length > MaxBioLength)
            errors.Add(BioError.TooLongCode());

        if (stageName.Length < MinStageNameLength)
            errors.Add(StageNameError.StageNameTooShort());

        if (stageName.Length > MaxStageNameLength)
            errors.Add(StageNameError.StageNameTooLong());

        if (errors.Any())
            return Result.Fail(errors);

        return new ArtistDescription
        {
            Bio = bio,
            StageName = stageName,
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return StageName;
        yield return Bio;
    }
}
