using DomainSeed;

namespace StemService.Domain.Errors;

public class InvalidOperationErrors : ErrorBase
{

    public const int InvalidOperationGroupCode = 50;

    private InvalidOperationErrors(string message, int code) : base(message, code, InvalidOperationGroupCode) { }

    public static ErrorBase MusicFileWasNotInitialized() => new InvalidOperationErrors("music file was not initialized", 1);

}
