using FluentResults;

namespace PermissionService.Domain.ProjectPermissions.Repositories;

public interface IUserProjectPermissionRepository
{

    public Result<UserProjectPermission> AddProjectPermission(UserProjectPermission projectPermission);
    public Result ChangeProjectPermission(UserProjectPermission projectPermission);
    public Result<IEnumerable<UserProjectPermission>> GetProjectPermissions(Guid projectId);
    public Result<IEnumerable<UserProjectPermission>> GetUserPermissions(Guid userId);

}
