using FluentResults;
using PermissionService.Domain.UserPermissions.Entities;
using PermissionService.Domain.UserPermissions.Errors;

namespace PermissionService.Domain.UserPermissions;

public class UserPermission
{

    public string UserId { get; init; } // keeping this as a string for unique identifier including an email or something else
    public string Password { get; init; }
    public Permissions Permission { get; private set; }

    private UserPermission() { }

    public static UserPermission Create(string userId)
    {
        return new UserPermission() { UserId = userId, Permission = Permissions.Guest };
    }

    public Result Authorize()
    {
        if (Permission != Permissions.Guest)
            return InvalidOperationError.InvalidTransitionOfPermission();
    
        Permission = Permissions.Normal;

        return Result.Ok();
    }

    public Result Elevate()
    {
        if(Permission != Permissions.Normal)
            return InvalidOperationError.InvalidTransitionOfPermission();

        Permission = Permissions.Verified;

        return Result.Ok();
    }
}
