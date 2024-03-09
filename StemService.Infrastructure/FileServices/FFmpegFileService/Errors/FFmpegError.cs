using FluentResults;
using StemService.Infrastructure.FileServices.Core.Errors;

namespace StemService.Infrastructure.FileServices.FFmpegFileService.Errors;

public class FFmpegError : GeneralError
{

    private FFmpegError(string message) : base(message)
    {
    }

    public static Error FailedCreateFile(string error) => new FFmpegError($"exception thrown while creating file. error(s) - '{error}'");

    public static Error FailedToTranscode(string error) => new FFmpegError($"exception thrown while transcoding file. error(s) - '{error}'");
    
    public static Error FailedToGenerateAmplitudePoints(string error) => new FFmpegError($"exception thrown while generating ampliture points. error(s) - '{error}'");

    public static Error FailedToCloseUpStreams(string error) => new FFmpegError($"exception thrown while cleaning up streams. error(s) - '{error}'");
    
    public static Error FailedToDeleteFile(string error) => new FFmpegError($"exception thrown while deleting file. error(s) - '{error}'");

    public static Error FailedToOpenFile(string error) => new FFmpegError($"exception thrown while open file. error(s) - '{error}'");

}
