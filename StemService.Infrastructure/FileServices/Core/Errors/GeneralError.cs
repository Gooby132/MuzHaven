using FluentResults;

namespace StemService.Infrastructure.FileServices.Core.Errors;

public class GeneralError : Error
{

    public GeneralError(string message) : base($"General Error - '{message}'")
    {
        
    }

}
