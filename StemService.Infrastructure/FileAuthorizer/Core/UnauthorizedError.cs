using FluentResults;

namespace StemService.Infrastructure.FileAuthorizer.Core;

public class UnauthorizedError : Error
{

    public UnauthorizedError(string message) : base($"unauthorized error - '{message}'")
    {
        
    }

}
