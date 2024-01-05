using DomainSeed.ValueObjects.Auth;
using DomainSeed.ValueObjects.Internet;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PermissionService.Infrastructure.Authorization.Abstracts;
using PermissionService.Infrastructure.Authorization.JwtProvider.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PermissionService.Infrastructure.Authorization.JwtProvider.Core;

internal class JwtTokenProvider : IPermissionTokenProvider
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
        _logger.LogTrace("{this} create commenter was requested for user - '{userId}' and project - '{project}'",
            this, userId, project);

        var issuer = _config.Issuer;
        var audience = _config.Audience;
        var key = Encoding.ASCII.GetBytes(_config.Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(IPermissionTokenProvider.PermissionTypeClaim, Domain.ProjectPermissions.ValueObjects.Permissions.Commenter.Name),
                new Claim(IPermissionTokenProvider.UserIdClaim, userId.ToString()),
                new Claim(IPermissionTokenProvider.ProjectIdClaim, project.ToString()),
                new Claim(ClaimTypes.Role, Domain.ProjectPermissions.ValueObjects.Permissions.Commenter.Name),
            }),
            Expires = DateTime.UtcNow.AddDays(15),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)
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
                new Claim(IPermissionTokenProvider.PermissionTypeClaim, Domain.ProjectPermissions.ValueObjects.Permissions.Contributer.Name),
                new Claim(IPermissionTokenProvider.UserIdClaim, userId.ToString()),
                new Claim(IPermissionTokenProvider.ProjectIdClaim, project.ToString()),
                new Claim(ClaimTypes.Role, Domain.ProjectPermissions.ValueObjects.Permissions.Contributer.Name),
            }),
            Expires = DateTime.UtcNow.AddDays(15),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new Token(tokenHandler.WriteToken(token));
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
                new Claim(IPermissionTokenProvider.PermissionTypeClaim, Domain.ProjectPermissions.ValueObjects.Permissions.Reader.Name),
                new Claim(IPermissionTokenProvider.UserIdClaim, userId.ToString()),
                new Claim(IPermissionTokenProvider.ProjectIdClaim, project.ToString()),
                new Claim(ClaimTypes.Role, Domain.ProjectPermissions.ValueObjects.Permissions.Reader.Name),
            }),
            Expires = DateTime.UtcNow.AddDays(15),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new Token(tokenHandler.WriteToken(token));
    }

    public Token CreateGuestToken(
        string userId,
        string firstName,
        string lastName,
        string email,
        string stageName)
    {
        var issuer = _config.Issuer;
        var audience = _config.Audience;
        var key = Encoding.ASCII.GetBytes(_config.Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(IPermissionTokenProvider.PermissionTypeClaim, Domain.UserPermissions.ValueObjects.Permissions.Guest.Name),
                new Claim(IPermissionTokenProvider.UserIdClaim, userId),
                new Claim(IPermissionTokenProvider.FirstNameClaim, firstName),
                new Claim(IPermissionTokenProvider.LastNameClaim, lastName),
                new Claim(IPermissionTokenProvider.EmailClaim, email),
                new Claim(IPermissionTokenProvider.StageClaim, stageName),
                new Claim(ClaimTypes.Role, Domain.UserPermissions.ValueObjects.Permissions.Guest.Name)
            }),
            Expires = DateTime.UtcNow.AddDays(15),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)

        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);
        return new Token(stringToken);
    }

    public Token CreateAdminToken()
    {
        var issuer = _config.Issuer;
        var audience = _config.Audience;
        var key = Encoding.ASCII.GetBytes(_config.Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(IPermissionTokenProvider.PermissionTypeClaim, Domain.UserPermissions.ValueObjects.Permissions.Admin.Name),
                new Claim(ClaimTypes.Role, Domain.UserPermissions.ValueObjects.Permissions.Admin.Name)
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)

        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);
        return new Token(stringToken);
    }
}
