// Ignore Spelling: Jwt

using FluentResults;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StemService.Domain.Services;
using StemService.Infrastructure.FileAuthorizer.Core;
using StemService.Infrastructure.FileAuthorizer.JwtAuthorizer.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace StemService.Infrastructure.FileAuthorizer.JwtAuthorizer.Core;

internal class JwtTokenProvider : IFileAuthorizer
{
    private readonly ILogger<JwtTokenProvider> _logger;
    private readonly JwtProviderOptions _config;

    public JwtTokenProvider(
        ILogger<JwtTokenProvider> logger,
        IOptions<JwtProviderOptions> options)
    {
        _logger = logger;
        _config = options.Value;
    }

    public async Task<Result<string>> GenerateKeyForStems(IEnumerable<Guid> stemsIds, CancellationToken cancellationToken = default)
    {
        _logger.LogTrace("{this} create reader was requested for stems - '{stemsIds}'",
            this, string.Join(", ", stemsIds));

        var issuer = _config.Issuer;
        var audience = _config.Audience;
        var key = Encoding.ASCII.GetBytes(_config.Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                stemsIds.Select((stemId, i) => new Claim($"{IFileAuthorizer.StemClaimName}-{i}", stemId.ToString()))),
            Expires = DateTime.UtcNow.AddMinutes(15),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Result.Ok(tokenHandler.WriteToken(token));
    }

    public async Task<Result<IEnumerable<Guid>>> ParseAuthorizedKey(ClaimsPrincipal user, CancellationToken token = default)
    {
        if (user is null)
            return Result.Fail(new UnauthorizedError("claims were not received"));

        var stemClaims = user.Claims
            .Where(claim => claim.Type.StartsWith(IFileAuthorizer.StemClaimName))
            .Select(claim => claim.Value);

        var stemGuids = new List<Guid>();

        foreach (var claim in stemClaims)
        {
            if(Guid.TryParse(claim, out var stemGuid))
            {
                stemGuids.Add(stemGuid);
            }
        }

        return Result.Ok<IEnumerable<Guid>>(stemGuids);
    }
}
