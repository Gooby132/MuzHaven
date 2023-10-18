using FluentResults;
using PermissionService.Domain.ProjectPermissions.Errors;
using PermissionService.Domain.ProjectPermissions.ValueObjects;

namespace PermissionService.Domain.ProjectPermissions;

public class UserProjectPermission
{

    public Guid UserId { get; init; }
    public Guid ProjectId { get; init; }
    public Permissions Permission { get; private set; }

    private UserProjectPermission () { }
    
    public static UserProjectPermission Create(Guid userId, Guid projectId, Permissions permission)
    {
        return new UserProjectPermission
        {
            UserId = userId,
            ProjectId = projectId,
            Permission = permission
        };
    }

    public Result MakeContributer()
    {
        if(Permission == Permissions.Creator)
            return InvalidOperationError.InvalidTransitionOfPermission();

        Permission = Permissions.Contributer;

        return Result.Ok();
    }

    public Result MakeReader()
    {
        if (Permission == Permissions.Creator)
            return InvalidOperationError.InvalidTransitionOfPermission();

        Permission = Permissions.Reader;

        return Result.Ok();
    }

    public Result MakeCommenter()
    {
        if (Permission == Permissions.Creator)
            return InvalidOperationError.InvalidTransitionOfPermission();

        Permission = Permissions.Commenter;

        return Result.Ok();
    }

}
