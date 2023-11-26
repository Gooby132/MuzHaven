using FluentResults;

namespace DomainSeed;

public class ErrorBase : Error
{

    public int ErrorCode { get; init; }
    public int GroupCode { get; init; }

    public ErrorBase(string message, int code, int groupCode) : base(message)
    {
        ErrorCode = code;
        GroupCode = groupCode;
    }
}
