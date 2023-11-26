using DomainSeed;

namespace PermissionService.Domain.UserPermissions.Errors;

public static class PasswordErrors
{
    public const int PasswordGroupError = 1;

    public static ErrorBase PasswordTooShort() => new ErrorBase("Password too short", 1, PasswordGroupError);

}
