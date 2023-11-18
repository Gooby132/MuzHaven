using FluentResults;

namespace StemService.Domain.Repositories;

public interface IStemRepository
{

    public Task<Result<Stem>> Upload(Stem stem, CancellationToken token = default);
    public Task<Result<IEnumerable<Stem>>> GetStemsByProjectId(Guid projectId,CancellationToken token = default);
    public Task<Result<Stem>> GetStemById(Guid stemId, CancellationToken token = default);
    public Task<Result<IEnumerable<Stem>>> GetAllStems(CancellationToken cancellationToken);
}
