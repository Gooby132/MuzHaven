using DomainSeed;
using FluentResults;

namespace ProjectService.Domain.Errors;

public class DescriptionError : ErrorBase
{

    public const int MaximumDescriptionLength = 2;

    private DescriptionError(string message, int code) : base(message, code, MaximumDescriptionLength)
    {
    }

    public static Error MaximumDescriptionError() => new DescriptionError("Description too long", 0);
    public static Error MinimumDescriptionError() => new DescriptionError("Description too short", 1);

}
