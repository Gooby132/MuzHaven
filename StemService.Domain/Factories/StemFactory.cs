using FluentResults;
using StemService.Domain.Services;

namespace StemService.Domain.Factories;

internal class StemFactory
{
    private readonly IFileService _fileService;

    public StemFactory(IFileService fileService)
    {
        _fileService = fileService;
    }

    public async Task<Result<Stem>> CreateStem(Guid projectId, Guid userId, Stream stream, CancellationToken token = default)
    {
        var stem = await _fileService.SaveMediaFile(stream, token);

        if (stem.IsFailed)
            Result.Fail(stem.Errors);

        return new Stem
        {
            ProjectId = projectId,
            UserId = userId,
            MediaFile = stem.Value
        };
    }

}
