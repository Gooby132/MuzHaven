using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using PermissionService.Domain.UserPermissions;
using PermissionService.Domain.UserPermissions.Repositories;

namespace ApiService.Application.Permissions.Queries;

public static class GetPermissionByUserId
{

    public class Query : IRequest<Result<UserPermission>>
    {
        public string UserId { get; init; }
        public string Password { get; init; }
    }

    internal class Handler : IRequestHandler<Query, Result<UserPermission>>
    {
        private readonly ILogger<Handler> _logger;
        private readonly IUserPermissionRepository _repository;

        public Handler(ILogger<Handler> logger, IUserPermissionRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Result<UserPermission>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("{this} get permission for user with id - '{userId}' was requested",
                this, request.UserId);

            var permission = await _repository.GetUserPermissionByEmail(request.UserId, cancellationToken);

            if (permission.IsFailed)
            {
                _logger.LogError("{this} failed to fetch permission. error(s) - '{errors}'",
                    this, string.Join(", ", permission.Reasons.Select(r => r.Message)));

                return Result.Fail(permission.Errors);
            }

            var verified = permission.Value.Authorize(request.Password);
            if (verified.IsFailed)
            {
                _logger.LogTrace("{this} user id - '{userId}' is not authorize",
                    this, request.UserId);

                return Result.Fail(verified.Errors);
            }

            _logger.LogDebug("{this} get permission for user id - '{userId}' was verified",
                this, request.UserId);

            return Result.Ok(permission.Value);
        }
    }

}
