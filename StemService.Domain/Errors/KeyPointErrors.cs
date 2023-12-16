using DomainSeed;

namespace StemService.Domain.Errors;

public class KeyPointErrors : ErrorBase
{
    public const int KeyPointGroupError = 2;

    private KeyPointErrors(string message, int code) : base(message, code, KeyPointGroupError) { }

    public static ErrorBase KeyPointMessageTooLong() => new KeyPointErrors("message too long", 1);
    public static ErrorBase KeyPointMessageTooShort() => new KeyPointErrors("message too short", 2);

}
