using DomainSeed.CommonErrors;
using DomainSeed.ValueObjects.Auth;
using DomainSeed.ValueObjects.Internet;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserService.Domain;
using UserService.Domain.Errors;
using UserService.Domain.Repositories;
using UserService.Persistence.Context;
using UserService.Persistence.Errors;

namespace UserService.Persistence.Repositories;

internal class UserRepository : IUserRepository
{

    #region Fields
    private readonly ILogger<UserRepository> _logger;
    private readonly UserContext _context;

    #endregion

    #region Properties

    public string Name { get; set; } = nameof(UserRepository);

    #endregion

    public UserRepository(ILogger<UserRepository> logger, UserContext userContext)
    {
        _logger = logger;
        _context = userContext;
    }

    public async Task<Result<User>> GetUserById(Guid id, CancellationToken token = default)
    {
        try
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id, token);

            if (user is null)
                return new NotFoundError();

            return user;
        }
        catch (Exception e)
        {
            return Result.Fail(new DatabaseError(e));
        }
    }

    public async Task<Result> RegisterUser(User user, CancellationToken token = default)
    {
        try
        {
            await _context.Users.AddAsync(user, token);
            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(new DatabaseError(e));
        }
    }

    public async Task<Result<User>> GetUserByEmail(Email email, CancellationToken token)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.MetaData.Email.Raw == email.Raw, token);

            if (user is null)
                return new NotFoundError();

            return Result.Ok(user);
        }
        catch (Exception e)
        {
            return Result.Fail(new DatabaseError(e));
        }
    }

    public override string ToString() => Name;
}
