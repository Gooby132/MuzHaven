using FluentResults;
using ProjectService.Domain.Errors;

namespace ProjectService.Domain.ValueObjects;

public class MusicalProfile 
{

    public MusicalKey Key { get; init; }
    public MusicalScale Scale { get; init; }

    private MusicalProfile(
        MusicalKey key,
        MusicalScale scale)
    {
        Key = key;
        Scale = scale;
    }

    public static Result<MusicalProfile?> Create(int? key, int? scale)
    {

        if (!key.HasValue && !scale.HasValue)
            return Result.Ok<MusicalProfile?>(null);

        var errors = new List<Error>();

        if (!Enum.IsDefined(typeof(MusicalKey), key.Value))
            errors.Add(MusicalProfileError.KeyIsNotDefined());

        if (!Enum.IsDefined(typeof(MusicalScale), scale.Value))
            errors.Add(MusicalProfileError.ScaleIsNotDefined());

        if (errors.Any())
            return Result.Fail(errors);

        return new MusicalProfile((MusicalKey)key.Value, (MusicalScale)scale.Value);
    }

    public enum MusicalKey
    {
        None = -1,
        C,
        CSharp_DFlat,
        D,
        DSharp_EFlat,
        E,
        F,
        FSharp_GFlat,
        G,
        GSharp_AFlat,
        A,
        ASharp_BFlat,
        B
    }

    public enum MusicalScale
    {
        None = -1,
        Major,
        NaturalMinor,
        HarmonicMinor,
        MelodicMinor,
        Blues,
        PentatonicMajor,
        PentatonicMinor,
        Dorian,
        Mixolydian,
        Phrygian,
        Locrian,
        WholeTone,
        Chromatic
    }

}
