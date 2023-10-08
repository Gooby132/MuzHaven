using DomainSeed;
using FluentResults;
using UserService.Domain.Errors;

namespace UserService.Domain.ValueObjects;

public class PersonMetaData : ValueObject
{

    public const string DefaultImage = "default-user.png";
    public const int MinNameLength = 2;
    public const int MaxNameLength = 30;

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    private PersonMetaData() { }

    private PersonMetaData(
        string firstName,
        string lastName,
        string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public static Result<PersonMetaData> Create(
        string firstName,
        string lastName,
        string email)
    {

        if (firstName.Length < MinNameLength)
            return new InvalidNameError(InvalidNameError.ErrorCodes.NameTooShortCode);

        if (firstName.Length > MaxNameLength)
            return new InvalidNameError(InvalidNameError.ErrorCodes.NameTooLongCode);
        
        if (lastName.Length < MinNameLength)
            return new InvalidNameError(InvalidNameError.ErrorCodes.NameTooShortCode);

        if (lastName.Length > MaxNameLength)
            return new InvalidNameError(InvalidNameError.ErrorCodes.NameTooLongCode);

        return new PersonMetaData(
            firstName, 
            lastName, 
            email);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
        yield return Email;
    }
}
