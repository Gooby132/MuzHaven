using DomainSeed.ValueObjects.Internet;
using FluentResults;

namespace UserService.Domain.Repositories;

public interface IUserRepository
{
    public Task<Result<User>> GetUserByEmail(Email email, CancellationToken cancellationToken);
    public Task<Result<User>> GetUserById(Guid id, CancellationToken token = default);

    public Task<Result> RegisterUser(
        User user, 
        CancellationToken token = default);

}
