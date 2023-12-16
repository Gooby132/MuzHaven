using DomainSeed;

namespace StemService.Domain.Errors;

public class DescriptionErrors : ErrorBase
{

    public const int DescriptionGroupError = 1;

    private DescriptionErrors(string message, int code) : base(message, code, DescriptionGroupError) { }

    public static ErrorBase DescriptionTooLong() => new DescriptionErrors("description is too long", 1);
    public static ErrorBase DescriptionTooShort() => new DescriptionErrors("description is too short", 2);

}
