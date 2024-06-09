using DomainSeed;

namespace ProjectService.Domain.Errors;

public class UnautherizedError : ErrorBase
{

    public const int UnautherizedGroupError = 5;

    public UnautherizedError(string message, int code) : base(message, code, UnautherizedGroupError)
    {
    }
}
