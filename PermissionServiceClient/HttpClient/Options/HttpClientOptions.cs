using System.ComponentModel.DataAnnotations;

namespace PermissionServiceClient.HttpClient.Options;

public class HttpClientOptions
{
    public const string Key = "Options";

    [Required]
    public string BaseUri { get; set; }

    [Required]
    public int Timeout { get; set; }

}
