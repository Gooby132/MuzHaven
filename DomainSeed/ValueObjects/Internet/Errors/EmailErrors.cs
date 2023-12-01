namespace DomainSeed.ValueObjects.Internet.Errors;

public class EmailErrors : ErrorBase
{

    public const int EmailGroupError = 2;

    private EmailErrors(string message, int code) : base(message, code, EmailGroupError) { }

    public static ErrorBase EmailIsInvalid() => new EmailErrors("Email is not valid", 1);
    public static ErrorBase EmailWasNotFound() => new EmailErrors("Email was not found", 2);

}
