using DomainSeed;

namespace UserService.Domain.Errors;

public class StageNameError : ErrorBase
{
    public const int StageNameGroupCode = 6;

    private StageNameError(string message, int code) : base(message, code, StageNameGroupCode) { }

    public static ErrorBase StageNameTooShort() => new StageNameError("Stage name too short", 1);
    public static ErrorBase StageNameTooLong() => new StageNameError("Stage name too long", 2);

}
