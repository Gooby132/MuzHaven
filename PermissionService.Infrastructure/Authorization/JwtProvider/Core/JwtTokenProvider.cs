// Ignore Spelling: Jwt

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PermissionService.Infrastructure.Authorization.Abstracts;
using PermissionService.Domain.UserPermissions.ValueObjects;
using PermissionService.Infrastructure.Authorization.Core;
using PermissionService.Infrastructure.Authorization.JwtProvider.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PermissionService.Infrastructure.Authorization.JwtProvider.Core;

internal class JwtTokenProvider : IAuthorizationTokenProvider
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

    public Token CreateCommenterToken(string userId, Guid project)
    {
        var issuer = _config.Issuer;
        var audience = _config.Audience;
        var key = Encoding.ASCII.GetBytes(_config.Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("type", "commenter"),
                new Claim("userId", userId.ToString()),
                new Claim("projectId", project.ToString()),
            }),
            Expires = DateTime.UtcNow.AddMinutes(15),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new Token(tokenHandler.WriteToken(token));
    }

    public Token CreateContributerToken(string userId, Guid project)
    {
        var issuer = _config.Issuer;
        var audience = _config.Audience;
        var key = Encoding.ASCII.GetBytes(_config.Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("type", "contributer"),
                new Claim("userId", userId.ToString()),
                new Claim("projectId", project.ToString()),
            }),
            Expires = DateTime.UtcNow.AddMinutes(15),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new Token(tokenHandler.WriteToken(token));
    }

    public Token CreateGuestToken(string userId)
    {
        var issuer = _config.Issuer;
        var audience = _config.Audience;
        var key = Encoding.ASCII.GetBytes(_config.Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(IAuthorizationTokenProvider.PermissionTypeClaim, Permissions.Guest.Name),
                new Claim(IAuthorizationTokenProvider.UserIdClaim, userId.ToString()),
                new Claim(ClaimTypes.Role, Permissions.Guest.Name)
            }),
            Expires = DateTime.UtcNow.AddMinutes(15),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);
        return new Token(stringToken);
    }

    public Token CreateReaderToken(string userId, Guid project)
    {
        var issuer = _config.Issuer;
        var audience = _config.Audience;
        var key = Encoding.ASCII.GetBytes(_config.Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("type", "reader"),
                new Claim("userId", userId.ToString()),
                new Claim("projectId", project.ToString()),
            }),
            Expires = DateTime.UtcNow.AddMinutes(15),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new Token(tokenHandler.WriteToken(token));
    }
}
