using DomainSeed;

namespace UserService.Domain.Errors;

public class BioError : ErrorBase
{
    public const int 
        BioGeneralCode = 1,
        BioTooShortCode = 2,
        BioTooLongCode = 3,
        BioGroupError = 5;

    private BioError(string message, int code) : base(message, code, BioGroupError) { }

    public static ErrorBase GeneralError(string message) => new BioError(message, 1);
    public static ErrorBase TooShortCode() => new BioError("Bio is too short", BioTooShortCode);
    public static ErrorBase TooLongCode() => new BioError("Bio is too long", BioTooLongCode);

}
