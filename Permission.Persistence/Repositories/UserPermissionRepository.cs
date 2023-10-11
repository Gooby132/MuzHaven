using FluentResults;
using Microsoft.EntityFrameworkCore;
using PermissionService.Domain.Errors;
using PermissionService.Domain.UserPermissions;
using PermissionService.Domain.UserPermissions.Repositories;
using PermissionService.Persistence.Context;
using PermissionService.Persistence.Errors;

namespace PermissionService.Persistence.Repositories;

internal class UserPermissionRepository : IUserPermissionRepository
{
    private readonly PermissionContext _context;

    public UserPermissionRepository(PermissionContext context)
    {
        _context = context;
    }

    public async Task<Result<UserPermission>> GetUserPermission(string userId, CancellationToken token = default)
    {
        try
        {
            var project = await _context.UserPermissions.FirstOrDefaultAsync(p => p.UserId == userId, token);

            if (project is null)
                return new NotFoundError();

            return project;
        }
        catch (Exception e)
        {
            return Result.Fail(new DatabaseError(e));
        }
    }

    public async Task<Result<UserPermission>> Register(UserPermission userPermission, CancellationToken token = default)
    {
        try
        {
            await _context.UserPermissions.AddAsync(userPermission, token);

            return Result.Ok(userPermission);
        }
        catch (Exception e)
        {
            return Result.Fail(new DatabaseError(e));
        }
    }

}
