using ApiService.Application.Dispatcher;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using UserService.Domain;
using ApiService.Application.Permissions.Queries;
using ApiService.Application.Users.Queries;
using DomainSeed.ValueObjects.Auth;
using PermissionService.Domain.UserPermissions.ValueObjects;
using PermissionService.Domain.UserPermissions;

namespace ApiService.Application.Users.Commands;

public static class LoginUserByEmail
{

    public record Command(string Email, string Password) : IRequest<Result<(User User, UserPermission Permission)>>;

    internal class Handler : IRequestHandler<Command, Result<(User User, UserPermission Permission)>>
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

        public async Task<Result<(User User, UserPermission Permission)>> Handle(Command request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("{this} register user was requested",
                this);

            var user = _mediator.Send(new GetUserByEmail.Query(request.Email), cancellationToken);

            var permission = _mediator.Send(new GetPermissionByUserId.Query
            {
                Email = request.Email,
                Password = request.Password
            });

            Task.WaitAll(permission, user);

            if (permission.Result.IsFailed)
            {
                _logger.LogWarning("{this} failed to fetch for userId permission - '{permission}'",
                    this, request.Email);

                return Result.Fail(permission.Result.Errors);
            }

            if (user.Result.IsFailed)
            {
                _logger.LogWarning("{this} failed to fetch user by userId - '{id}'",
                    this, request.Email);

                return Result.Fail(user.Result.Errors);
            }
          
            _logger.LogDebug("{this} register user was successful",
                this);

            await _mediator.DispatchDomainEvents(user.Result.Value, cancellationToken);

            return Result.Ok<(User User, UserPermission Permission)>(new(user.Result.Value, permission.Result.Value));
        }
    }
}
