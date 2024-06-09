using DomainSeed.ValueObjects.Auth;

namespace PermissionService.Infrastructure.Authorization.Abstracts;

public interface IPermissionTokenProvider
{

    public const string PermissionSchemeName = "Permission";

    public const string PermissionTypeClaim = "type";
    public const string PermissionAdminRoleTypeClaim = "Admin";
    public const string UserIdClaim = "user-id";
    public const string FirstNameClaim = "first-name";
    public const string LastNameClaim = "last-name";
    public const string EmailClaim = "user-email";
    public const string StageClaim = "stage";
    
    public const string ProjectIdClaim = "project-id";

    public Token CreateGuestToken(
        string userId,
        string firstName,
        string lastName,
        string email,
        string stageName);
    public Token CreateReaderToken(string userId, Guid project);
    public Token CreateCommenterToken(string userId, Guid project);
    public Token CreateContributerToken(string userId, Guid project);
    public Token CreateAdminToken();

}
