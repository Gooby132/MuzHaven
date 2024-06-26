﻿using FluentResults;
using Microsoft.Extensions.Options;
using StemService.Domain.Services;
using StemService.Domain.ValueObjects;
using StemService.Infrastructure.FileServices.Core.Errors;
using StemService.Infrastructure.FileServices.LocalFileService.Errors;
using StemService.Infrastructure.FileServices.LocalFileService.Options;

namespace StemService.Infrastructure.FileServices.LocalFileService.Core;

public class LocalFileSystemService : IFileService
{
    private readonly LocalFileServiceOptions _config;

    public DirectoryInfo LocalFileDirectory { get; }
    public const string StemDirectoryName = "stems";

    public LocalFileSystemService(
        IOptions<LocalFileServiceOptions> options)
    {
        _config = options.Value;
        LocalFileDirectory = Directory.CreateDirectory(Path.Combine(_config.BaseDirectory, StemDirectoryName));
    }

    public async Task<Result<MusicFile>> SaveMediaFile(Stream stream, string fileName, CancellationToken token = default)
    {
        try
        {
            string randName = Path.GetRandomFileName();

            using var write = File.Create(Path.Combine(LocalFileDirectory.FullName, randName));

            await stream.CopyToAsync(write, token);

            stream.Dispose();

            if (token.IsCancellationRequested)
                return Result.Fail(new CanceledError());

            var musicFile = MusicFile.Create
            (
                randName,
                write.Length,
                Path.GetExtension(fileName).Substring(1),
                ""
            );

            return musicFile;
        }
        catch (Exception e)
        {
            return Result.Fail(new LocalIOError(e));
        }
    }

    public Task<Result> RemoveMediaFile(string fileName)
    {
        try
        {
            File.Delete(Path.Combine(LocalFileDirectory.FullName, fileName));

            return Task.FromResult(Result.Ok());
        }
        catch (Exception e)
        {
            return Task.FromResult(Result.Fail(new LocalIOError(e)));
        }
    }

    public Task<Result<Stream>> GetStem(string fileName, CancellationToken token = default)
    {
        try
        {
            return Task.FromResult<Result<Stream>>(File.OpenRead(Path.Combine(LocalFileDirectory.FullName, fileName)));
        }
        catch (Exception e)
        {
            return Task.FromResult<Result<Stream>>(Result.Fail(new LocalIOError(e)));
        }
    }

}
