using DomainSeed;
using FluentResults;

namespace StemService.Domain.ValueObjects;

public class MusicFile : Entity<string>
{

    public string Path { get; init; }
    public int Length { get; init; }
    public string Format { get; init; }
    public string AudioQuality { get; init; }
    public ICollection<AmplitudePoint>? AmplitudesPoints { get; private set; }

    private MusicFile() { }

    public static Result<MusicFile> Create(
        string path,
        int length,
        string format,
        string audioQuality)
    {
        return Result.Ok(new MusicFile
        {
            Path = path,
            Length = length,
            AudioQuality = audioQuality,
            Format = format,
        });
    }

    public Result AddAmplitudesPoints(ICollection<AmplitudePoint> amplitudePoints)
    {
        AmplitudesPoints = amplitudePoints;

        return Result.Ok();
    }

}
