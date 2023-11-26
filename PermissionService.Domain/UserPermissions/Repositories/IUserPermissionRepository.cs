using FluentResults;

namespace PermissionService.Domain.UserPermissions.Repositories;

public interface IUserPermissionRepository
{

    public Task<Result<UserPermission>> Permit(UserPermission userPermission,CancellationToken token = default);
    public Task<Result<UserPermission>> GetUserPermissionByEmail(string userId,CancellationToken token = default);

}
