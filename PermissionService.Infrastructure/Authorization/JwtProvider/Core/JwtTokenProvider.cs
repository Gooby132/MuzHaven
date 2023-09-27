// Ignore Spelling: Jwt

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PermissionService.Infrastructure.Authorization.Abstracts;
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

    public Token CreateCommenterToken(Guid userId, Guid project)
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

    public Token CreateContributerToken(Guid userId, Guid project)
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

    public Token CreateGuestToken(Guid userId)
    {
        var issuer = _config.Issuer;
        var audience = _config.Audience;
        var key = Encoding.ASCII.GetBytes(_config.Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("type", "guest"),
                new Claim("userId", userId.ToString()),
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

    public Token CreateReaderToken(Guid userId, Guid project)
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
