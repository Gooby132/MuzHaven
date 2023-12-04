﻿using ApiService.Application.Dispatcher;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectService.Domain;
using ProjectService.Domain.Context;
using ProjectService.Domain.Repositories;
using ProjectService.Domain.ValueObjects;

namespace ApiService.Application.Projects.Commands;

public static class CreateProject
{

    public record Command(
        string Title,
        string Album,
        string Description,
        string ReleaseInUtc,
        float BeatsPerMinute,
        int MusicalKey,
        int MusicalScale
        ) : IRequest<Result<Project>>;

    internal class Handler : IRequestHandler<Command, Result<Project>>
    {

        #region Fields

        private readonly ILogger<Handler> _logger;
        private readonly IProjectRepository _repository;
        private readonly IProjectUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        #endregion

        #region Properties

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

        public async Task<Result<Project>> Handle(Command request, CancellationToken cancellationToken)
        {

            _logger.LogTrace("{this} create project was requested",
                this);

            var project = Project.Create(
                request.Title,
                request.Album,
                request.Description,
                request.ReleaseInUtc,
                request.BeatsPerMinute,
                request.MusicalKey,
                request.MusicalScale
                );

            if (project.IsFailed)
            {
                _logger.LogTrace("{this} bad request creating project. error(s) - '{errors}'",
                    this, string.Join(", ", project.Reasons.Select(r => r.Message)));

                return Result.Fail(project.Errors);
            }

            var create = await _repository.CreateProject(project.Value, cancellationToken);

            if (create.IsFailed)
            {
                _logger.LogError("{this} (infrastructure) failed to create project. error(s) - '{errors}'",
                    this, string.Join(", ", create.Reasons.Select(r => r.Message)));

                return Result.Fail(create.Errors);
            }

            var persist = await _unitOfWork.CommitAsync(cancellationToken);

            if (persist.IsFailed)
            {
                _logger.LogError("{this} (infrastructure) failed to persist project. error(s) - '{errors}'",
                    this, string.Join(", ", persist.Reasons.Select(r => r.Message)));

                return Result.Fail(persist.Errors);
            }

            _logger.LogDebug("{this} register project was successful",
                this);

            await _mediator.DispatchDomainEvents(project.Value, cancellationToken);

            return Result.Ok(project.Value);
        }
    }

}
