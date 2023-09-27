using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using UserService.Domain;
using UserService.Domain.Errors;
using UserService.Domain.Repositories;
using UserService.Persistence.Repositories;

namespace UserService.Application.Users.Queries;

public static class GetUserByIdQuery
{

    public class Query : IRequest<Result<User>>
    {
        public Guid Id { get; init; }
    }

    public class Handler : IRequestHandler<Query, Result<User>>
    {
        #region Fields

        private readonly ILogger<Handler> _logger;
        private readonly IUserRepository _userRepository;

        #endregion

        #region Properties

        public string Name { get; set; } = nameof(Handler);

        #endregion

        public Handler(ILogger<Handler> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<Result<User>> Handle(Query request, CancellationToken cancellationToken)
        {

            _logger.LogTrace("{this} query for user with id - '{id}' was made",
                this, request.Id);

            var res = await _userRepository.GetUserById(request.Id, cancellationToken);

            if (res.IsFailed)
            {
                if (res.HasError<NotFoundError>())
                {
                    _logger.LogWarning("{this} user with id - '{id}' was not found",
                        this, request.Id);

                    return Result.Fail(res.Errors);
                }

                _logger.LogError("{this} (infrastructure) failed to fetch user with id - '{id}'. error(s) - '{errors}'",
                    this, request.Id, string.Join(", ", res.Reasons.Select(r => r.Message)));

                return Result.Fail(res.Errors);
            }

            _logger.LogDebug("{this} query for user with id - '{id}' made successfully",
                this, request.Id);

            return Result.Ok(res.Value);
        }

        public override string ToString() => Name;
    }
}
