using ApiService.Application.Dispatcher;
using ApiService.Application.Projects.Queries;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectService.Domain.Context;
using ProjectService.Domain.Repositories;

namespace ApiService.Application.Projects.Commands;

public static class DeleteProjectCommand
{

    public class Command : IRequest<Result>
    {
        public int ProjectId { get; init; }
        public string CreatorId { get; init; }
    }

    public class Handler : IRequestHandler<Command, Result>
    {

        #region Fields

        private readonly ILogger<Handler> _logger;
        private readonly IProjectRepository _repository;
        private readonly IProjectUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        #endregion

        #region Constructor

        public Handler(ILogger<Handler> logger, IProjectRepository repository, IProjectUnitOfWork unitOfWork, IMediator mediator)
        {
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        #endregion

        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("{this} create project was requested",
                this);

            var project = await _mediator.Send(new GetProjectByIdQuery.Query { Id = request.ProjectId });

            if (project.IsFailed)
            {
                _logger.LogError("{this} (infrastructure) failed to fetch project with id - {id}. error(s) - '{errors}'",
                    this, request.ProjectId, string.Join(", ", project.Reasons.Select(r => r.Message)));

                return Result.Fail(project.Errors);
            }

            project.Value.Delete(request.CreatorId);

            var delete = await _repository.DeleteProject(project.Value, cancellationToken);

            if (delete.IsFailed)
            {
                _logger.LogError("{this} (infrastructure) failed to delete project with id - {id}. error(s) - '{errors}'",
                    this, request.ProjectId, string.Join(", ", delete.Reasons.Select(r => r.Message)));

                return Result.Fail(project.Errors);
            }

            var persist = await _unitOfWork.CommitAsync(cancellationToken);

            if (persist.IsFailed)
            {
                _logger.LogError("{this} (infrastructure) failed to delete project. error(s) - '{errors}'",
                    this, string.Join(", ", persist.Reasons.Select(r => r.Message)));

                return Result.Fail(persist.Errors);
            }

            _logger.LogDebug("{this} delete project was successful",
                this);

            await _mediator.DispatchDomainEvents(project.Value, cancellationToken);

            return Result.Ok();
        }
    }
}
