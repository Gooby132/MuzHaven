using FluentResults;

namespace PermissionService.Domain.UserPermissions.Repositories;

public interface IUserPermissionRepository
{

    public Task<Result<UserPermission>> Register(UserPermission userPermission,CancellationToken token = default);
    public Task<Result<UserPermission>> GetUserPermission(string userId,CancellationToken token = default);

}
