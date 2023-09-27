using DomainSeed;
using FluentResults;

namespace UserService.Persistence.Context;

internal class UnitOfWork : IUnitOfWork
{
    private readonly UserContext _context;

    public UnitOfWork(UserContext context)
    {
        _context = context;
    }

    public Result Commit()
    {
        return Result.Try(() =>
        {
            _context.SaveChanges();
        });
    }

    public async Task<Result> CommitAsync(CancellationToken token = default)
    {
        return await Result.Try(() => {
            return (Task)_context.SaveChangesAsync(token);
        });
    }

    public Result Dispose()
    {
        return Result.Try(() =>
        {
            _context.Dispose();
        });
    }

    public async Task<Result> DisposeAsync()
    {
        return await Result.Try(() =>
        {
           return _context.DisposeAsync();
        });
    }
}
