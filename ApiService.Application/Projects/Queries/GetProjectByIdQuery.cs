﻿using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectService.Domain;
using ProjectService.Domain.Repositories;
using UserService.Domain.Repositories;

namespace ApiService.Application.Projects.Queries;

public static class GetProjectByIdQuery
{

    public class Query : IRequest<Result<Project>>
    {
        public int Id { get; init; }
    }

    public class Handler : IRequestHandler<Query, Result<Project>>
    {
        private readonly ILogger<Handler> _logger;
        private readonly IProjectRepository _repository;

        public string Name { get; set; } = nameof(GetProjectByIdQuery);

        public Handler(ILogger<Handler> logger, IProjectRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Result<Project>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("{this} project was requested by id - '{projectId}'",
                this, request.Id);

            var project = await _repository.GetProjectById(request.Id, cancellationToken);

            if (project.IsFailed)
            {
                _logger.LogError("{this} (infrastructure) project requested could not be fetched. error(s) - '{errors}'",
                    this, request.Id);

                return Result.Fail(project.Errors);
            }

            return Result.Ok(project.Value);
        }

        public override string ToString() => Name;
 
    }
}
