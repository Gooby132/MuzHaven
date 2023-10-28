using FluentResults;
using PermissionService.Contracts.Requests;
using PermissionService.Contracts.Responses;

namespace PermissionServiceClient.Abstractions;

public interface IPermissionServiceClient
{

    public Task<Result<UserRegistrationResponse>> Register(
        CreatePermissionRequest request,
        CancellationToken cancellationToken = default);

}
