namespace ProjectService.Contracts.Dtos;

public class CompleteProjectDto
{
    public Guid Id { get; init; }
    public string CreatedInUtc { get; init; }
    public string Title { get; init; }
    public string Album { get; init; }
    public string Description { get; init; }
    public string? ReleaseInUtc { get; init; }
    public float BeatsPerMinute { get; init; }
    public MusicalProfileDto? MusicalProfile { get; init; }

}
