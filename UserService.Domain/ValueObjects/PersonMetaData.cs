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

        List<Error> errors = new List<Error>();

        if (firstName.Length < MinNameLength)
            errors.Add(InvalidNameError.FirstNameTooShort());

        if (firstName.Length > MaxNameLength)
            errors.Add(InvalidNameError.FirstNameTooLong());
        
        if (lastName.Length < MinNameLength)
            errors.Add(InvalidNameError.LastNameTooShort());

        if (lastName.Length > MaxNameLength)
            errors.Add(InvalidNameError.LastNameTooLong());

        if (errors.Any())
            return Result.Fail(errors);

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
