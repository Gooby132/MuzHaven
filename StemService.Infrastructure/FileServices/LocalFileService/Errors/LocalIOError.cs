using FluentResults;
using StemService.Infrastructure.FileServices.Core.Errors;

namespace StemService.Infrastructure.FileServices.LocalFileService.Errors;

internal class LocalIOError : GeneralError
{
    public LocalIOError(Exception e) : base(e.Message)
    {
        Exception = e;
    }

    public Exception Exception { get; }
}
