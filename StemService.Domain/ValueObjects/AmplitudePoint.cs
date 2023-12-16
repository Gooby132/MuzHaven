using FluentResults;

namespace StemService.Domain.ValueObjects;

public class AmplitudePoint
{
    public const int MaximumMessageLength = 30;
    public const int MinimumMessageLength = 2;

    public Guid StemId { get; init; }
    public int Time { get; init; }
    public int Amplitude { get; init; }

    private AmplitudePoint() { }

    public static Result<AmplitudePoint> Create(int time, int amplitude)
    {
        return Result.Ok(new AmplitudePoint
        {
            Amplitude = amplitude,
            Time = time,
        });
    }

}
