using FluentResults;
using StemService.Domain.ValueObjects;

namespace StemService.Domain.Services;

public interface IFileService
{
    public Task<Result> RemoveMediaFile(string fileName);

    public Task<Result<MusicFile>> SaveMediaFile(Stream stream, string fileName, CancellationToken token = default);

    public Task<Result<Stream>> GetStem(string fileName, CancellationToken token = default);

}
