using FluentResults;

namespace DomainSeed;

public class ErrorBase : Error
{

    public int ErrorCode { get; init; }

    public ErrorBase(string message, int code) : base(message)
    {
        ErrorCode = code;
    }
}
