using FluentResults;
using StemService.Domain.Errors;
using StemService.Domain.Repositories;
using StemService.Domain.Services;
using System.Security.Claims;

namespace StemService.Domain.Factories;

public class StemPersistenceService
{
    private readonly IFileService _fileService;
    private readonly IFileAuthorizer _authorizer;
    private readonly IStemRepository _stemRepository;

    public StemPersistenceService(IFileService fileService, IFileAuthorizer authorizer, IStemRepository stemRepository)
    {
        _fileService = fileService;
        _authorizer = authorizer;
        _stemRepository = stemRepository;
    }

    public async Task<Result<Stem>> CreateStem(
        Guid projectId,
        Guid userId,
        Stream stream,
        string name,
        string instrument,
        CancellationToken token = default)
    {
        var stem = await _fileService.SaveMediaFile(stream, token);

        if (stem.IsFailed)
            Result.Fail(stem.Errors);

        return new Stem
        {
            ProjectId = projectId,
            UserId = userId,
            MediaFile = stem.Value,
            Name = name,
            Instrument = instrument,
        };
    }

    public async Task<Result> RemoveStem(Stem stem)
    {
        return await _fileService.RemoveMediaFile(stem.MediaFile);
    }

    public async Task<Result<(Stem Stem,Stream Stream)>> OpenStemStream(ClaimsPrincipal claims, Guid stemId, CancellationToken token = default)
    {
        var stem = await _stemRepository.GetStemById(stemId, token);

        if (stem.IsFailed)
            return Result.Fail(stem.Errors);

        var isAuthed = await _authorizer.ParseAuthorizedKey(claims);

        if (isAuthed.IsFailed)
            return Result.Fail(isAuthed.Errors);

        if (!isAuthed.Value.Contains(stem.Value.Id))
            return new NotAuthorizedError();

        var stream = await _fileService.GetStem(stem.Value.MediaFile, token);

        if (stream.IsFailed)
            return Result.Fail(stream.Errors);

        return (stem.Value, stream.Value);
    }

    public async Task<Result<string>> GenerateKeyForStemsByProjectId(Guid projectId, CancellationToken token = default)
    {
        var stems = await _stemRepository.GetStemsByProjectId(projectId, token);

        if (stems.IsFailed)
            return Result.Fail(stems.Errors);

        var key = await _authorizer.GenerateKeyForStems(stems.Value.Select(s => s.Id));

        if (key.IsFailed)
            return Result.Fail(key.Errors);

        return key;
    }

}
