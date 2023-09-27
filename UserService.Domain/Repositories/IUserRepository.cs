using FluentResults;

namespace UserService.Domain.Repositories;

public interface IUserRepository
{
    public Task<Result<User>> GetUserById(Guid id, CancellationToken token = default);

    public Task<Result> RegisterUser(
        User user, 
        CancellationToken token = default);

}
