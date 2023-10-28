using DomainSeed;
using FluentResults;

namespace PermissionService.Domain.UserPermissions.Errors;

public class InvalidOperationError : ErrorBase
{
    public const int InvalidChangeOfPermission = 1;

    private InvalidOperationError(string message, int code) : base(message, code) { }

    public static InvalidOperationError InvalidTransitionOfPermission() => new InvalidOperationError("Cannot change permission", InvalidChangeOfPermission);

}
