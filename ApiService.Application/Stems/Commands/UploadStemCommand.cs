using ApiService.Application.Dispatcher;
using DomainSeed;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using StemService.Domain;
using StemService.Domain.Factories;
using StemService.Domain.Repositories;
using StemService.Domain.ValueObjects;
using StemService.Persistence.Context;

namespace ApiService.Application.Stems.Commands;

public static class UploadStemCommand
{

    public record Command(
        int ProjectId,
        Guid UserId,
        Stream StemFile,
        string FileName,
        string Name,
        string Instrument,
        string? Description
        ) : IRequest<Result<Stem>>;

    public class Handler : IRequestHandler<Command, Result<Stem>>
    {

        #region Fields

        private readonly ILogger<Handler> _logger;
        private readonly StemPersistenceService _stemFactory;
        private readonly IStemRepository _stemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        #endregion

        #region Constructor
        public Handler(ILogger<Handler> logger, StemPersistenceService stemFactory, IStemRepository stemRepository, IStemUnitOfWork unitOfWork, IMediator mediator)
        {
            _logger = logger;
            _stemFactory = stemFactory;
            _stemRepository = stemRepository;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        #endregion

        #region Properties
        public async Task<Result<Stem>> Handle(Command request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("{this} stem upload was requested for project - '{project}' by user - '{user}'",
                this, request.ProjectId, request.UserId);

            var description = Description.Create(request.Description);

            if (description.IsFailed)
                return Result.Fail(description.Errors);

            var stem = await _stemFactory.CreateStem(
                request.ProjectId,
                request.UserId,
                request.StemFile,
                request.FileName,
                description.Value,
                request.Name,
                request.Instrument,
                cancellationToken);

            if (stem.IsFailed)
            {
                _logger.LogError("{this} failed to create stem for project - '{projectId}' by user - '{user}'. error(s) - '{errors}'", 
                    this, request.ProjectId, request.UserId, string.Join(", ", stem.Reasons.Select(r => r.Message)));

                return Result.Fail(stem.Errors);
            }

            var persist = await _stemRepository.Upload(stem.Value, cancellationToken);

            if (persist.IsFailed)
            {
                _logger.LogError("{this} failed to persist stem for project - '{projectId}' by user - '{user}'. error(s) - '{errors}'",
                    this, request.ProjectId, request.UserId, string.Join(", ", stem.Reasons.Select(r => r.Message)));

                await _stemFactory.RemoveStem(stem.Value);

                return Result.Fail(stem.Errors);
            }

            var uow = await _unitOfWork.CommitAsync(cancellationToken);

            if (uow.IsFailed)
            {
                _logger.LogError("{this} failed to commit transaction stem for project - '{projectId}' by user - '{user}'. error(s) - '{errors}'",
                    this, request.ProjectId, request.UserId, string.Join(", ", stem.Reasons.Select(r => r.Message)));

                await _stemFactory.RemoveStem(stem.Value);

                return Result.Fail(uow.Errors);
            }

            _logger.LogDebug("{this} stem upload was requested for project - '{project}' by user - '{user}' was successful",
                this, request.ProjectId, request.UserId);

            await _mediator.DispatchDomainEvents(stem.Value);

            return Result.Ok(stem.Value);
        }

        public override string ToString() => nameof(UploadStemCommand);

        #endregion

    }

}
