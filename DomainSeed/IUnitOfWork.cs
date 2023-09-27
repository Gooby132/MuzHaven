using FluentResults;

namespace DomainSeed;

public interface IUnitOfWork
{
    public Task<Result> CommitAsync(CancellationToken cancellationToken = default);
    public Result Commit();
    public Task<Result> DisposeAsync();
    public Result Dispose();
}
