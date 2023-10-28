using FluentResults;

namespace StemService.Domain.Services;

public interface IFileService
{

    public Task<Result<string>> SaveMediaFile(Stream stream, CancellationToken token = default);

}
