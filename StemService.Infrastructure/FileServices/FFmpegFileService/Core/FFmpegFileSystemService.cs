using FFMpegCore;
using FFMpegCore.Pipes;
using FluentResults;
using Microsoft.Extensions.Options;
using StemService.Domain.Services;
using StemService.Domain.ValueObjects;
using StemService.Infrastructure.FileServices.FFmpegFileService.Errors;
using StemService.Infrastructure.FileServices.FFmpegFileService.Options;

namespace StemService.Infrastructure.FileServices.FFmpegFileService.Core;

internal class FFmpegFileSystemService : IFileService
{
    private readonly FFmpegFileServiceOptions _config;

    public DirectoryInfo LocalFileDirectory { get; }
    public const string StemDirectoryName = "stems";

    public string Name { get; set; } = nameof(FFmpegFileSystemService);

    public FFmpegFileSystemService(
        IOptions<FFmpegFileServiceOptions> options)
    {
        _config = options.Value;
        LocalFileDirectory = Directory.CreateDirectory(Path.Combine(_config.BaseDirectory, StemDirectoryName));
    }

    public async Task<Result<Stream>> GetStem(string fileName, CancellationToken token = default)
    {
        try
        {
            return File.OpenRead(Path.Combine(LocalFileDirectory.FullName, fileName));
        }
        catch (Exception e)
        {
            return Result.Fail(FFmpegError.FailedToDeleteFile(e.Message));
        }
    }

    public async Task<Result> RemoveMediaFile(string fileName)
    {
        try
        {
            File.Delete(Path.Combine(LocalFileDirectory.FullName, fileName));

            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(FFmpegError.FailedToDeleteFile(e.Message));
        }
    }

    public async Task<Result<MusicFile>> SaveMediaFile(Stream stream, string fileName, CancellationToken token = default)
    {

        FileInfo file;
        FileStream write;

        // create local file
        try
        {
            file = new FileInfo(
                Path.Combine(
                LocalFileDirectory.FullName,
                Path.GetRandomFileName())
            );

            write = file.Create();

        }
        catch (Exception e)
        {
            return Result.Fail(FFmpegError.FailedCreateFile(e.Message));
        }

        var memory = new MemoryStream(20_000);
        try
        {
            if (!await FFMpegArguments
                .FromPipeInput(new StreamPipeSource(stream))
                .OutputToPipe(new StreamPipeSink(memory), args => {
                    args.ForceFormat("wav");
                })
                .ProcessAsynchronously())
                return Result.Fail(FFmpegError.FailedToTranscode("unknown"));

            stream.Dispose();
        }
        catch (Exception e)
        {
            return Result.Fail(FFmpegError.FailedToTranscode(e.Message));
        }


        // create amplitude points
        var amplitudesPoints = new List<AmplitudePoint>();
        try
        {
            int sections = 100;

            var buffer = memory.ToArray();

            for (int section = 0; section < sections; section++)
            {

                int sum = 0;

                for (int i = 0; i < (buffer.Length / sections ); i = i + 2)
                {
                    sum += ( buffer[i] + buffer[i + 1]);
                }

                amplitudesPoints.Add(new AmplitudePoint
                {
                    Amplitude = (sum / (buffer.Length / sections)) / (byte.MaxValue * 2f),
                    Section = section,
                    Sections = sections
                });
            }

        }
        catch (Exception e)
        {
            return Result.Fail(FFmpegError.FailedToGenerateAmplitudePoints(e.Message));
        }

        // writing to file
        try
        {
            memory.WriteTo(write);
            await memory.DisposeAsync();
            await write.DisposeAsync();

        }
        catch (Exception e)
        {
            return Result.Fail(FFmpegError.FailedToCloseUpStreams(e.Message));
        }

        var musicFile = MusicFile.Create(file.Name, 0, "wav", "ULTRA");

        if (musicFile.IsFailed)
            return musicFile;

        musicFile.Value.AddAmplitudesPoints(amplitudesPoints);

        return musicFile;
    }
}
