using ProjectService.Contracts.Dtos;

namespace ProjectService.Contracts.Requests;

public class CreateProjectRequest
{
    public string Token { get; set; }
    public ProjectDto Project { get; set; }
}
