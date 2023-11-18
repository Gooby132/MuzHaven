using DomainSeed;
using FluentResults;

namespace StemService.Persistence.Context;

internal class UnitOfWork : IStemUnitOfWork
{
    private readonly StemContext _context;

    public UnitOfWork(StemContext context)
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
