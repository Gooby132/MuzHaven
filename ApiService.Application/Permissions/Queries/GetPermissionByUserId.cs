using DomainSeed.ValueObjects.Auth;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using PermissionService.Domain.UserPermissions;
using PermissionService.Domain.UserPermissions.Repositories;
using PermissionService.Infrastructure.Authorization.Abstracts;

namespace ApiService.Application.Permissions.Queries;

public static class GetPermissionByUserId
{

    public class Query : IRequest<Result<(UserPermission UserPermission, Token Token)>>
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }

    internal class Handler : IRequestHandler<Query, Result<(UserPermission UserPermission, Token Token)>>
    {
        private readonly ILogger<Handler> _logger;
        private readonly IUserPermissionRepository _repository;
        private readonly IPermissionTokenProvider _tokenProvider;

        public string Name { get; set; } = nameof(GetPermissionByUserId);

        public Handler(
            ILogger<Handler> logger,
            IUserPermissionRepository repository,
            IPermissionTokenProvider tokenProvider)
        {
            _logger = logger;
            _repository = repository;
            _tokenProvider = tokenProvider;
        }

        public async Task<Result<(UserPermission UserPermission, Token Token)>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("{this} get permission for user with id - '{userId}' was requested",
                this, request.Email);

            var permission = await _repository.GetUserPermissionByEmail(request.Email, cancellationToken);

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
                    this, request.Email);

                return Result.Fail(verified.Errors);
            }

            _logger.LogDebug("{this} get permission for user id - '{userId}' was verified",
                this, request.Email);

            switch (permission.Value.Permission.Name)
            {
                case nameof(PermissionService.Domain.UserPermissions.ValueObjects.Permissions.Guest):
                    return Result.Ok<(UserPermission UserPermission, Token Token)>(
                        new(permission.Value, _tokenProvider.CreateGuestToken(permission.Value.Email.Raw)));
                case nameof(PermissionService.Domain.UserPermissions.ValueObjects.Permissions.Normal):
                    return Result.Ok<(UserPermission UserPermission, Token Token)>(
                        new(permission.Value, _tokenProvider.CreateGuestToken(permission.Value.Email.Raw)));
                case nameof(PermissionService.Domain.UserPermissions.ValueObjects.Permissions.Verified):
                    return Result.Ok<(UserPermission UserPermission, Token Token)>(
                        new(permission.Value, _tokenProvider.CreateGuestToken(permission.Value.Email.Raw)));
                default:
                    break;
            }

            return Result.Fail("");
        }
        public override string ToString() => Name;

    }
}