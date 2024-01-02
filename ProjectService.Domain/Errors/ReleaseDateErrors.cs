using DomainSeed;

namespace ProjectService.Domain.Errors;

public class ReleaseDateErrors : ErrorBase
{

    public const int GroupCode = 4;

    private ReleaseDateErrors(string message, int code) : base(message, code, GroupCode) { }

    public static ErrorBase CouldNotParseReleaseDate() => new ReleaseDateErrors("Could not parse release date", 0);
    public static ErrorBase ReleaseDateSetToPast() => new ReleaseDateErrors("Release date cannot be set to past", 1);

}
