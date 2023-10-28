using FluentResults;
using UserService.Contracts.Requests;

namespace UserServiceClient.Abstractions;

public interface IUserServiceClient
{

    public Task<Result<RegisterRequest>> Register(
        RegisterRequest request,
        CancellationToken token = default);

}
