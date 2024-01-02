using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using StemService.Domain;
using StemService.Domain.Repositories;

namespace ApiService.Application.Stems.Queries;

public static class GetStemsByProjectIdQuery
{

	public record Query(int ProjectId) : IRequest<Result<IEnumerable<Stem>>>;

    public class Handler : IRequestHandler<Query, Result<IEnumerable<Stem>>>
    {

        #region Fields

        private readonly ILogger<Handler> _logger;
        private readonly IStemRepository _stemRepository;
        private readonly IMediator _mediator;

        #endregion

        #region Properties

        public string Name { get; set; }

        #endregion

        #region Constructor

        public Handler(ILogger<Handler> logger, IStemRepository stemRepository, IMediator mediator)
        {
            _logger = logger;
            _stemRepository = stemRepository;
            _mediator = mediator;
        }

        #endregion

        #region Methods

        public async Task<Result<IEnumerable<Stem>>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("{this} fetching stems of project with id - '{project}'",
                this, request.ProjectId);

            var res = await _stemRepository.GetStemsByProjectId(request.ProjectId, cancellationToken);

            if (res.IsFailed)
            {
                _logger.LogError("{this} failed fetching all stems by project id - '{projectId}'. error(s) - '{errors}'",
                    this, request.ProjectId, string.Join(", ", res.Reasons.Select(r => r.Message)));

                return Result.Fail(res.Errors);
            }

            _logger.LogTrace("{this} fetching stems of project with id - '{project}' was successful",
                this, request.ProjectId);

            return res;
        }

        #endregion
    }

}
