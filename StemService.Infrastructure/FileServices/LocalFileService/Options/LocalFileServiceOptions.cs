using System.ComponentModel.DataAnnotations;

namespace StemService.Infrastructure.FileServices.LocalFileService.Options;

public class LocalFileServiceOptions
{

    public const string Key = "LocalFileOptions";

    [Required(AllowEmptyStrings = false)]
    public string BaseDirectory { get; init; }
}
