using FluentResults;

namespace StemService.Domain.ValueObjects;

public class AmplitudePoint
{
    public const int MaximumMessageLength = 30;
    public const int MinimumMessageLength = 2;

    public Guid StemId { get; init; }
    public int Section { get; init; }
    public int Sections { get; init; }
    public float Amplitude { get; init; }

    public AmplitudePoint() { }

    public static Result<AmplitudePoint> Create(int time, int amplitude)
    {
        return Result.Ok(new AmplitudePoint
        {
            Amplitude = amplitude,
            Section = time,
        });
    }

}
