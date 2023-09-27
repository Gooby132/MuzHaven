using System.ComponentModel.DataAnnotations;

namespace PermissionService.Infrastructure.Authorization.JwtProvider.Options;

internal class JwtProviderOptions
{
    public const string OptionsKey = "Options";

    [Required]
    public string Key { get; init; }
    [Required]
    public string Issuer { get; init; }
    [Required]
    public string Audience { get; init; }
}
