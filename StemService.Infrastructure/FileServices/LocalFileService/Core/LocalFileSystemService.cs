using FluentResults;
using Microsoft.Extensions.Options;
using StemService.Domain.Services;
using StemService.Infrastructure.FileServices.Core.Errors;
using StemService.Infrastructure.FileServices.LocalFileService.Errors;
using StemService.Infrastructure.FileServices.LocalFileService.Options;

namespace StemService.Infrastructure.FileServices.LocalFileService.Core;

public class LocalFileSystemService : IFileService
{
    private readonly LocalFileServiceOptions _config;
    public const string StemDirectoryName = "stems";

    public LocalFileSystemService(IOptions<LocalFileServiceOptions> options)
    {
        _config = options.Value;
    }

    public async Task<Result<string>> SaveMediaFile(Stream stream, CancellationToken token = default)
    {
        try
        {
            string fileName = Path.GetTempFileName();
            var directory = Directory.CreateDirectory(Path.Combine(_config.BaseDirectory, StemDirectoryName));

            using var write = File.Create(Path.Combine(directory.FullName, fileName));

            await stream.CopyToAsync(write, token);

            stream.Dispose();

            if (token.IsCancellationRequested)
                return Result.Fail(new CanceledError());

            return Result.Ok(fileName);
        }
        catch (Exception e)
        {
            return Result.Fail(new LocalIOError(e));
        }
    }
}
