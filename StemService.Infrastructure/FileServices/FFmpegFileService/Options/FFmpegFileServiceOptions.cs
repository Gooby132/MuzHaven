using System.ComponentModel.DataAnnotations;

namespace StemService.Infrastructure.FileServices.FFmpegFileService.Options;

public class FFmpegFileServiceOptions
{

    public const string Key = "FFmpegFileOptions";

    [Required(AllowEmptyStrings = false)]
    public string BaseDirectory { get; init; } = default!;
}
