using DomainSeed;
using DomainSeed.ValueObjects.Internet;
using FluentResults;
using UserService.Domain.Errors;

namespace UserService.Domain.ValueObjects;

public class PersonMetaData : ValueObject
{

    public const string DefaultImage = "default-user.png";
    public const int MinNameLength = 2;
    public const int MaxNameLength = 30;

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Email Email { get; private set; }

    private PersonMetaData() { }

    private PersonMetaData(
        string firstName,
        string lastName,
        Email email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public static Result<PersonMetaData> Create(
        string? firstName,
        string? lastName,
        string? email)
    {

        if (string.IsNullOrEmpty(firstName)) return InvalidNameError.FirstNameWasEmpty();
        if (string.IsNullOrEmpty(lastName)) return InvalidNameError.LastNameWasEmpty();

        List<IError> errors = new List<IError>();

        if (firstName.Length < MinNameLength)
            errors.Add(InvalidNameError.FirstNameTooShort());

        if (firstName.Length > MaxNameLength)
            errors.Add(InvalidNameError.FirstNameTooLong());

        if (lastName.Length < MinNameLength)
            errors.Add(InvalidNameError.LastNameTooShort());

        if (lastName.Length > MaxNameLength)
            errors.Add(InvalidNameError.LastNameTooLong());

        var emailValueObject = Email.Create(email);

        if (emailValueObject.IsFailed)
        {
            errors.AddRange(emailValueObject.Errors);
        }

        if (errors.Any())
            return Result.Fail(errors);

        return new PersonMetaData(
            firstName,
            lastName,
            emailValueObject.Value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
        yield return Email;
    }
}
