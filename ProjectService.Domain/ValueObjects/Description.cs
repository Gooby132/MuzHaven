using FluentResults;
using ProjectService.Domain.Errors;

namespace ProjectService.Domain.ValueObjects;

public class Description
{

    public const int MaximumDescriptionLength = 300;
    public const int MinimumDescriptionLength = 1;

    public string Text { get; init; } = default!;

    // EF constructor
    private Description() { }
    
    public static Result<Description> Create(string text)
    {
        if (text.Length > MaximumDescriptionLength)
            return DescriptionError.MaximumDescriptionError();

        if (text.Length < MinimumDescriptionLength)
            return TitleError.MinimumTitleError();

        return new Description() { Text = text };
    }

}
