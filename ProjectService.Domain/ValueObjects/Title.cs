using FluentResults;
using ProjectService.Domain.Errors;

namespace ProjectService.Domain.ValueObjects;

public class Title
{
    public const int MaximumTitleLength = 50;
    public const int MinimumTitleLength = 1;

    public string Text { get; init; }

    private Title() { }

    public static Result<Title> Create(string text)
    {

        if (text.Length > MaximumTitleLength)
            return TitleError.MaximumTitleError();

        if (text.Length < MinimumTitleLength)
            return TitleError.MinimumTitleError();

        return new Title
        {
            Text = text,
        };
    }

}
