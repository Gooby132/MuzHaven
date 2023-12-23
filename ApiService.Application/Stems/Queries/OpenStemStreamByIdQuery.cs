using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using StemService.Domain;
using StemService.Domain.Factories;
using System.Security.Claims;

namespace ApiService.Application.Stems.Queries;

public static class OpenStemStreamByIdQuery
{

    public class Query : IRequest<Result<(Stem Stem, Stream Stream)>>
    {
        public ClaimsPrincipal User { get; }
        public Guid StemId { get; }

        public Query(ClaimsPrincipal user, Guid stemId)
        {
            User = user;
            StemId = stemId;
        }
    }

    internal class Handler : IRequestHandler<Query, Result<(Stem Stem, Stream Stream)>>
    {
        private readonly ILogger<Handler> _logger;
        private readonly StemPersistenceService _persistenceService;

        public Handler(ILogger<Handler> logger, StemPersistenceService persistenceService)
        {
            _logger = logger;
            _persistenceService = persistenceService;
        }

        public async Task<Result<(Stem Stem, Stream Stream)>> Handle(Query request, CancellationToken cancellationToken)
        {
            var stem = await _persistenceService.OpenStemStream(request.User, request.StemId, cancellationToken);

            if(stem.IsFailed)
            {
                _logger.LogWarning("{this} failed to fetch stem with stem id - '{stemId}'",
                    this, request.StemId);

                return Result.Fail(stem.Errors);
            }

            return stem;
        }
    }

}
