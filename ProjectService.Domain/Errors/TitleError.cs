using DomainSeed;
using FluentResults;

namespace ProjectService.Domain.Errors;

public class TitleError : ErrorBase
{
    public const int TitleGroup = 1;

    private TitleError(string message, int code) : base(message, code, TitleGroup) { }

    public static Error MaximumTitleError() => new TitleError("Title too long", 0);
    public static Error MinimumTitleError() => new TitleError("Title too short", 0);

}
