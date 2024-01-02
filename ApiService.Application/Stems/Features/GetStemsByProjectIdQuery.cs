using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using StemService.Domain.Factories;

namespace ApiService.Application.Stems.Features;

public static class GetStemsByProjectIdQuery
{

	public class Query : IRequest<Result<string>>
	{

        public int ProjectId { get; }

        public Query(int projectId)
        {
            ProjectId = projectId;
        }
    }

    class Handler : IRequestHandler<Query, Result<string>>
    {
        private readonly ILogger<Handler> _logger;
        private readonly StemPersistenceService _persistenceService;

        public Handler(ILogger<Handler> logger, StemPersistenceService persistenceService)
        {
            _logger = logger;
            _persistenceService = persistenceService;
        }
        public async Task<Result<string>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("{this} stems authorization by project id - '{projectId}' requested",
                this, request.ProjectId);

            var key = await _persistenceService.GenerateKeyForStemsByProjectId(request.ProjectId, cancellationToken);

            if (key.IsFailed)
            {
                _logger.LogWarning("{this} stems authorization by project id - '{projectId}' failed. error(s) - '{errors}'",
                    this, request.ProjectId, string.Join(", ", key.Reasons.Select(r => r.Message)));

                return key;
            }

            _logger.LogDebug("{this} stems authorization by project id - '{projectId}' was successful",
                this, request.ProjectId);

            return key;

        }
    }

}
