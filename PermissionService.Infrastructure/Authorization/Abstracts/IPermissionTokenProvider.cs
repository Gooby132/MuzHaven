using DomainSeed.ValueObjects.Auth;

namespace PermissionService.Infrastructure.Authorization.Abstracts;

public interface IPermissionTokenProvider
{

    public const string PermissionSchemeName = "Permission";

    public const string PermissionTypeClaim = "type";
    public const string UserIdClaim = "userId";
    public const string ProjectIdClaim = "projectId";

    public Token CreateGuestToken(string userId);
    public Token CreateReaderToken(string userId, Guid project);
    public Token CreateCommenterToken(string userId, Guid project);
    public Token CreateContributerToken(string userId, Guid project);

}
