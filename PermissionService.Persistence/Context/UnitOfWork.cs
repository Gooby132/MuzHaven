using FluentResults;
using PermissionService.Domain.UserPermissions.UnitOfWork;
using PermissionService.Persistence.Errors;

namespace PermissionService.Persistence.Context;

internal class UnitOfWork : IUserPermissionUnitOfWork
{
    private PermissionContext _context;

    public UnitOfWork(PermissionContext context)
    {
        _context = context;
    }

    public Result Commit()
    {
        try
        {
            _context.SaveChanges();
            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(new DatabaseError(e));
        }
    }

    public async Task<Result> CommitAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(new DatabaseError(e));
        }
    }

    public Result Dispose()
    {
        try
        {
            _context.Dispose();
            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(new DatabaseError(e));
        }
    }

    public async Task<Result> DisposeAsync()
    {
        try
        {
            await _context.DisposeAsync();
            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(new DatabaseError(e));
        }
    }
}
