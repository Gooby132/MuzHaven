using ProjectService.Contracts.Dtos;

namespace ProjectService.Contracts.Responses;

public class GetProjectsResponse
{

    public IEnumerable<CompleteProjectDto> Projects { get; set; }

}
