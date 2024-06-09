using System.ComponentModel.DataAnnotations;

namespace StemService.Infrastructure.FileAuthorizer.JwtAuthorizer.Options;

internal class JwtProviderOptions
{
    public const string OptionsKey = "Options";

    [Required]
    public string Key { get; init; } = default!;
    [Required]
    public string Issuer { get; init; } = default!;
    [Required]
    public string Audience { get; init; } = default!;
}
