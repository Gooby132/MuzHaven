using ApiService.Application.Dispatcher;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using UserService.Domain;
using ApiService.Application.Permissions.Queries;
using ApiService.Application.Users.Queries;
using DomainSeed.ValueObjects.Auth;

namespace ApiService.Application.Users.Commands;

public static class LoginUserByEmail
{

    public record Command(string Email, string Password) : IRequest<Result<(User User, Token Token)>>;

    internal class Handler : IRequestHandler<Command, Result<(User User, Token Token)>>
    {
        private readonly ILogger<Handler> _logger;
        private readonly IMediator _mediator;

        public Handler(
            ILogger<Handler> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<Result<(User User, Token Token)>> Handle(Command request, CancellationToken cancellationToken)
        {

            _logger.LogTrace("{this} register user was requested",
                this);

            var permission = await _mediator.Send(new GetPermissionByUserId.Query
            {
                Email = request.Email,
                Password = request.Password
            });

            if (permission.IsFailed)
            {
                _logger.LogWarning("{this} failed to fetch for userId permission - '{permission}'",
                    this, request.Email);

                return Result.Fail(permission.Errors);
            }

            var user = await _mediator.Send(new GetUserByEmail.Query(request.Email), cancellationToken);

            _logger.LogDebug("{this} register user was successful",
                this);

            await _mediator.DispatchDomainEvents(user.Value, cancellationToken);

            return Result.Ok<(User User, Token Token)>(new (user.Value, permission.Value.Token));
        }
    }
}
