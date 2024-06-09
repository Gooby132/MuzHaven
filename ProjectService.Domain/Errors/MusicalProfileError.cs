using DomainSeed;

namespace ProjectService.Domain.Errors;

public class MusicalProfileError : ErrorBase
{
    public const int MusicalProfileGroupCode = 3;

    private MusicalProfileError(string message, int code) : base(message, code, MusicalProfileGroupCode) { }

    public static MusicalProfileError KeyIsNotDefined() => new MusicalProfileError("Musical Key is not defined", 0);
    public static MusicalProfileError ScaleIsNotDefined() => new MusicalProfileError("Musical Scale is not defined", 1);
    public static MusicalProfileError KeyAndScaleMustBeBothPresent() => new MusicalProfileError("Key and scale must be both present", 2);

}
