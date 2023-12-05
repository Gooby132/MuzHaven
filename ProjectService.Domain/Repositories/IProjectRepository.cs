using FluentResults;

namespace ProjectService.Domain.Repositories;

public interface IProjectRepository
{

    public Task<Result<Project>> CreateProject(Project project,CancellationToken token = default);

    public Task<Result<Project>> GetProjectById(Guid projectId, CancellationToken token = default);
    public Task<Result<IEnumerable<Project>>> GetProjectsByCreatorId(string userId, CancellationToken cancellationToken);
}
