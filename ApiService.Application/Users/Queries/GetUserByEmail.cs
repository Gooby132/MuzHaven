using DomainSeed.ValueObjects.Internet;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using UserService.Domain;
using UserService.Domain.Repositories;

namespace ApiService.Application.Users.Queries;

public static class GetUserByEmail
{

    public record Query(string Email) : IRequest<Result<User>>;

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
            _logger.LogTrace("{this} fetching user with email - '{email}' was requested",
                this, request.Email);

            var email = Email.Create(request.Email);

            if (email.IsFailed)
            {
                _logger.LogWarning("{this} failed to create email for - '{email}' error(s) - '{errors}'",
                    this, request.Email, string.Join(", ", email.Reasons.Select(r => r.Message)));

                return Result.Fail(email.Errors);
            }

            var user = await _userRepository.GetUserByEmail(email.Value, cancellationToken);

            if (user.IsFailed)
            {
                _logger.LogWarning("{this} failed fetching user by email - '{email}'", 
                    this, request.Email);

                return user;
            }

            _logger.LogDebug("{this} fetching user with email - '{email}' was successful",
                this, request.Email);

            return user;
        }
    }
}
