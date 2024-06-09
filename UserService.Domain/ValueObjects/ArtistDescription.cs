using DomainSeed;
using FluentResults;
using UserService.Domain.Errors;

namespace UserService.Domain.ValueObjects;

public class ArtistDescription : ValueObject
{

    public const int MinBioLength = 2;
    public const int MaxBioLength = 300;

    public const int MinStageNameLength = 2;
    public const int MaxStageNameLength = 30;

    public string StageName { get; init; } = default!;
    public string? Bio { get; init; } 

    private ArtistDescription() { }

    public static Result<ArtistDescription> Create(string? stageName, string? bio)
    {
        List<Error> errors = new List<Error>();

        if (!string.IsNullOrEmpty(bio))
        {
            if (bio.Length < MinBioLength)
                errors.Add(BioError.TooShortCode());

            if (bio.Length > MaxBioLength)
                errors.Add(BioError.TooLongCode());
        }

        if (string.IsNullOrEmpty(stageName))
            return StageNameError.StageNameWasEmpty();

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

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return StageName;
        yield return Bio;
    }
}
