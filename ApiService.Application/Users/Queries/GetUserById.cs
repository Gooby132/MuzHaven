using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using UserService.Domain;
using UserService.Domain.Repositories;

namespace ApiService.Application.Users.Queries;

public static class GetUserById
{

	public class Query : IRequest<Result<User>>
	{
        public Guid Id { get; set; }
    }

    internal class Handler : IRequestHandler<Query, Result<User>>
    {
        private readonly ILogger<Handler> _logger;
        private readonly IUserRepository _userRepository;

        public Handler(ILogger<Handler> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<Result<User>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("{this} user was requested by id - '{userId}'",
                this, request.Id);

            var user = await _userRepository.GetUserById(request.Id, cancellationToken);

            if (user.IsFailed)
            {
                _logger.LogError("{this} (infrastructure) user requested could not be fetched. error(s) - '{errors}'",
                    this, request.Id);

                return Result.Fail(user.Errors);
            }

            return Result.Ok(user.Value);
        }
    }

}
