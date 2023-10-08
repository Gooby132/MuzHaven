using FluentResults;
using UserService.Contracts.Requests;
using UserServiceClient.Abstractions;

namespace UserServiceClient.HttpClient.Core;

public class UserServiceHttpClient : IUserServiceClient
{
    public Task<Result<RegisterRequest>> Register(RegisterRequest request, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}
