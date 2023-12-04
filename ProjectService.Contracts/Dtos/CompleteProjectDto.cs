namespace ProjectService.Contracts.Dtos;

public class CompleteProjectDto : ProjectDto
{
    public Guid Id { get; init; }
    public string CreatedInUtc { get; init; }

}
