using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using StemService.Domain;
using StemService.Domain.Repositories;

namespace ApiService.Application.Stems.Queries;

public static class GetAllQuery
{

	public class Query : IRequest<Result<IEnumerable<Stem>>>
	{

	}

    class Handler : IRequestHandler<Query, Result<IEnumerable<Stem>>>
    {
        private readonly ILogger<Handler> _logger;
        private readonly IStemRepository _stemRepository;

        public string Name { get; set; } = nameof(Handler);

        public Handler(ILogger<Handler> logger, IStemRepository stemRepository)
        {
            _logger = logger;
            _stemRepository = stemRepository;
        }

        public async Task<Result<IEnumerable<Stem>>> Handle(Query request, CancellationToken cancellationToken)
        {

            _logger.LogTrace("{this} request for all stems was requested",
                this);

            var all = await _stemRepository.GetAllStems(cancellationToken);

            if (all.IsFailed)
            {
                _logger.LogWarning("{this} fetching all stems failed. error(s) - '{errors}'",
                    this, string.Join(", ", all.Reasons.Select(r => r.Message)));

                return all;
            }

            _logger.LogDebug("{this} request for all stems was successful", 
                this);

            return all;
        }

        public override string ToString() => Name;

    }

}
