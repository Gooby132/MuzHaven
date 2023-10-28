using FluentResults;
using PermissionService.Contracts.Requests;
using PermissionService.Contracts.Responses;
using PermissionServiceClient.Abstractions;

namespace PermissionServiceClient.HttpClient.Core;

public class PermissionServiceHttpClient : IPermissionServiceClient
{
    public Task<Result<UserRegistrationResponse>> Register(CreatePermissionRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
