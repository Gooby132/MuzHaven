using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectService.Domain;
using ProjectService.Domain.Repositories;

namespace ApiService.Application.Projects.Queries;

public static class GetProjectsByCreatorId
{

    public record Query(string CreatorId) : IRequest<Result<IEnumerable<Project>>>;

    internal class Handler : IRequestHandler<Query, Result<IEnumerable<Project>>>
    {
        #region Fields

        private readonly ILogger<Handler> _logger;
        private readonly IProjectRepository _projectRepository;

        #endregion

        #region Properties

        public string Name { get; set; } = nameof(GetProjectsByCreatorId);

        #endregion

        public Handler(ILogger<Handler> logger, IProjectRepository projectRepository)
        {
            _logger = logger;
            _projectRepository = projectRepository;
        }

        public async Task<Result<IEnumerable<Project>>> Handle(Query request, CancellationToken cancellationToken)
        {

            _logger.LogTrace("{this} get projects by user id - '{userId}' was requested",
                this, request.CreatorId);

            var res = await _projectRepository.GetProjectsByCreatorId(request.CreatorId, cancellationToken);

            if (res.IsFailed)
            {
                _logger.LogError("{this} (infrastructure) failed to fetch projects by creator id - '{creatorId}'. error(s) - '{errors}'",
                    this, request.CreatorId, string.Join(", ", res.Reasons.Select(r => r.Message)));

                return Result.Fail(res.Errors);
            }

            _logger.LogDebug("{this} get projects by user id - '{userId}' was successful",
                this, request.CreatorId);

            return res;
        }
    }

}
