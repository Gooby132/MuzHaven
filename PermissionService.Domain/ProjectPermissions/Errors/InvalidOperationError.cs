using DomainSeed;

namespace PermissionService.Domain.ProjectPermissions.Errors;

public class InvalidOperationError : ErrorBase
{
    public const int PermissionOperationGroupError = 123;

    public const int ThisPermissionIsAlreadyTheCurrentPermissionCode = 1;
    public const int InvalidChangeOfPermissionCode = 2;

    private InvalidOperationError(string message, int code) : base(message, code, PermissionOperationGroupError) { }

    public static InvalidOperationError ThisPermissionIsAlreadyTheCurrentPermission() => new InvalidOperationError("Permission is already the requested permission", ThisPermissionIsAlreadyTheCurrentPermissionCode);
    public static InvalidOperationError InvalidTransitionOfPermission() => new InvalidOperationError("Cannot change permission", InvalidChangeOfPermissionCode);

}
