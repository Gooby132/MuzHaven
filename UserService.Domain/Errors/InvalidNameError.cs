using DomainSeed;
using FluentResults;

namespace UserService.Domain.Errors;

public class InvalidNameError : ErrorBase
{

    public const int InvalidFirstNameGroupCode = 3;
    public const int InvalidLastNameGroupCode = 4;

    private InvalidNameError(string message, int code, int group) : base(message, code, group) { }

    public static ErrorBase FirstNameTooShort() => new InvalidNameError("First name is too short", 1, InvalidFirstNameGroupCode);
    public static ErrorBase FirstNameTooLong() => new InvalidNameError("First name is too long", 2, InvalidFirstNameGroupCode);
    public static ErrorBase FirstNameWasEmpty() => new InvalidNameError("First name is empty", 3, InvalidFirstNameGroupCode);
    public static ErrorBase LastNameTooShort() => new InvalidNameError("Last name is too short", 1, InvalidLastNameGroupCode);
    public static ErrorBase LastNameTooLong() => new InvalidNameError("Last name is too long", 2, InvalidLastNameGroupCode);
    public static ErrorBase LastNameWasEmpty() => new InvalidNameError("Last name is empty", 3, InvalidLastNameGroupCode);


}
