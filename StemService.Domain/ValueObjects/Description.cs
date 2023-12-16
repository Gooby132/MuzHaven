using FluentResults;
using StemService.Domain.Errors;

namespace StemService.Domain.ValueObjects;

public class Description
{
    public const int MaxDescription = 30;
    public const int MinDescription = 3;

    public string? Text { get; init; }

    private Description() { }

    public static Result<Description> Create(string? text)
    {

        if (!string.IsNullOrEmpty(text))
        {
            if (text.Length > MaxDescription)
                return Result.Fail(DescriptionErrors.DescriptionTooLong());

            if (text.Length < MinDescription)
                return Result.Fail(DescriptionErrors.DescriptionTooShort());
        }

        return new Description
        {
            Text = text,
        };
    }

}
