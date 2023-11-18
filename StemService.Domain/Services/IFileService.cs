using FluentResults;

namespace StemService.Domain.Services;

public interface IFileService
{
    public Task<Result> RemoveMediaFile(string fileName);

    public Task<Result<string>> SaveMediaFile(Stream stream, CancellationToken token = default);

    public Task<Result<Stream>> GetStem(string fileName, CancellationToken token = default);

}
