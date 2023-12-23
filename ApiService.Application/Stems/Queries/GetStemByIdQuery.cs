using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using StemService.Domain;
using StemService.Domain.Repositories;

namespace ApiService.Application.Stems.Queries;

public static class GetStemByIdQuery
{

	public class Query : IRequest<Result<Stem>>
	{
        public Guid StemId { get; }

        public Query(Guid stemId)
        {
            StemId = stemId;
        }
    }

    internal class Handler : IRequestHandler<Query, Result<Stem>>
    {
        private readonly ILogger<Handler> _logger;
        private readonly IStemRepository _stemRepository;

        public Handler(ILogger<Handler> logger, IStemRepository stemRepository)
        {
            _logger = logger;
            _stemRepository = stemRepository;
        }

        public async Task<Result<Stem>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("{this} requesting stem with id - '{stemId}'",
                this, request.StemId);

            var stem = await _stemRepository.GetStemById(request.StemId, cancellationToken);

            if (stem.IsFailed)
            {
                _logger.LogWarning("{this} failed to fetch stem by id - '{stemId}'. error(s) - '{errors}'",
                    this, request.StemId, string.Join(", ", stem.Reasons.Select(r => r.Message)));

                return Result.Fail(stem.Errors);
            }

            return stem;
        }
    }
}
