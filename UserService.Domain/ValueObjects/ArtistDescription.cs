using DomainSeed;
using FluentResults;
using UserService.Domain.Errors;

namespace UserService.Domain.ValueObjects;

public class ArtistDescription : ValueObject
{

    public const int MinBioLength = 2;
    public const int MaxBioLength = 150;

    public string StageName { get; init; }
    public string Bio { get; init; }

    private ArtistDescription(string stageName, string bio)
    {
        StageName = stageName;
        Bio = bio;
    }

    public static Result<ArtistDescription> Create(string stageName, string bio)
    {
        if (bio.Length < MinBioLength)
            return new InvalidBioError(InvalidBioError.ErrorCodes.BioTooShortCode);

        if (bio.Length > MaxBioLength)
            return new InvalidBioError(InvalidBioError.ErrorCodes.BioTooLongCode);

        return new ArtistDescription(stageName, bio);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return StageName; 
        yield return Bio;
    }
}
