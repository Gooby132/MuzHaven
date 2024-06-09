using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectService.Domain;
using ProjectService.Domain.Repositories;

namespace ApiService.Application.Projects.Queries;

public static class GetAllProjectsQuery
{

    public record Query : IRequest<Result<IEnumerable<Project>>>;

    internal class Handler : IRequestHandler<Query, Result<IEnumerable<Project>>>
    {
        private readonly ILogger<Handler> _logger;
        private readonly IProjectRepository _repository;

        public string Name { get; set; } = nameof(GetAllProjectsQuery);

        public Handler(ILogger<Handler> logger, IProjectRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Result<IEnumerable<Project>>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("{this} get all projects was requested",
                this);

            var projects = await _repository.GetAllProjects(cancellationToken);

            if (projects.IsFailed)
            {
                _logger.LogError("{this} (infrastructure) projects requested could not be fetched. error(s) - '{errors}'",
                    this);

                return Result.Fail(projects.Errors);
            }

            return Result.Ok(projects.Value);
        }

        public override string ToString() => Name;

    }

}
