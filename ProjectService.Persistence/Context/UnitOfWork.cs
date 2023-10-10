using FluentResults;
using ProjectService.Persistence.Errors;

namespace ProjectService.Persistence.Context;

internal class UnitOfWork : IProjectUnitOfWork
{
    private ProjectContext _context;

    public UnitOfWork(ProjectContext context)
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
