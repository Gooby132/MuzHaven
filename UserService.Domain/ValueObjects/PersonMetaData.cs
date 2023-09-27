using DomainSeed;
using FluentResults;
using UserService.Domain.Errors;

namespace UserService.Domain.ValueObjects;

public class PersonMetaData : ValueObject
{

    public const string DefaultImage = "default-user.png";
    public const int MinNameLength = 2;
    public const int MaxNameLength = 30;
    public const int MinBioLength = 2;
    public const int MaxBioLength = 150;

    public string Name { get; set; }
    public string Bio { get; set; }
    public string Image { get; set; }

    private PersonMetaData() { }

    private PersonMetaData(
        string name,
        string bio,
        string image)
    {
        Name = name;
        Bio = bio;
        Image = image;
    }

    public static Result<PersonMetaData> Create(
        string name,
        string bio,
        string? image)
    {

        if (name.Length < MinNameLength)
            return new InvalidNameError(InvalidNameError.ErrorCodes.NameTooShortCode);

        if (name.Length > MaxNameLength)
            return new InvalidNameError(InvalidNameError.ErrorCodes.NameTooLongCode);

        if (bio.Length < MinBioLength)
            return new InvalidBioError(InvalidBioError.ErrorCodes.BioTooShortCode);

        if (bio.Length > MaxBioLength)
            return new InvalidBioError(InvalidBioError.ErrorCodes.BioTooLongCode);

        return new PersonMetaData(
            name, 
            bio, 
            image ?? DefaultImage);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Bio;
        yield return Image;
    }
}
