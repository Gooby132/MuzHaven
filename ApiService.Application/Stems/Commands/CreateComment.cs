using ApiService.Application.Dispatcher;
using ApiService.Application.Stems.Queries;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using StemService.Domain.Repositories;
using StemService.Persistence.Context;

namespace ApiService.Application.Stems.Commands;

public static class CreateComment
{

    public record Request(Guid StemId, Guid CommenterId, string Text, int? Time) : IRequest<Result>;

    public class Handler : IRequestHandler<Request, Result>
    {

        private readonly ILogger<Handler> _logger;
        private readonly IStemRepository _stemRepository;
        private readonly IStemUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public string Name { get; set; } = nameof(Handler);

        public Handler(
            ILogger<Handler> logger,
            IStemRepository stemRepository,
            IStemUnitOfWork unitOfWork,
            IMediator mediator
            )
        {
            _logger = logger;
            _stemRepository = stemRepository;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var stem = await _mediator.Send(new GetStemById.Query(request.StemId));

            if (stem.IsFailed)
            {
                _logger.LogWarning("{this} failed to fetch stem with id - '{stem}'",
                    this, request.StemId);

                return Result.Fail(stem.Errors);
            }

            var res = stem.Value.CreateComment(request.CommenterId, request.Text, request.Time);

            if (res.IsFailed)
            {
                _logger.LogTrace("{this} failed to create comment for stem with id - '{stem}'. error(s) - '{errors}'",
                    this, request.StemId, string.Join(", ", res.Reasons.Select(r => r.Message)));

                return Result.Fail(res.Errors);
            }

            var persist = await _unitOfWork.CommitAsync(cancellationToken);

            if (persist.IsFailed)
            {
                _logger.LogError("{this} (infrastructure) failed to persist stem. error(s) - '{errors}'",
                    this, string.Join(", ", persist.Reasons.Select(r => r.Message)));

                return Result.Fail(persist.Errors);
            }

            await _mediator.DispatchDomainEvents(stem.Value, cancellationToken);

            return Result.Ok();
        }

        public override string ToString() => Name;

    }
}
