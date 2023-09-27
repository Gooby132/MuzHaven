using DomainSeed;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using UserService.Domain;
using UserService.Domain.Repositories;
using UserService.Domain.ValueObjects;

namespace UserService.Application.Users.Command;

public static class RegisterUserCommand
{

    public class Command : IRequest<Result<User>>
    {
        public string? Image { get; init; }
        public string Bio { get; init; }
        public string Name { get; init; }
    }

    internal class Handler : IRequestHandler<Command, Result<User>>
    {

        #region Fields

        private readonly ILogger<Handler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Properties

        public string Name { get; set; } = nameof(RegisterUserCommand);

        #endregion

        public Handler(ILogger<Handler> logger, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<User>> Handle(Command request, CancellationToken cancellationToken)
        {

            _logger.LogTrace("{this} handling request to register user", 
                this);

            var metaData = PersonMetaData.Create(
                request.Name,
                request.Bio,
                request.Image
                );

            if(metaData.IsFailed)
            {
                _logger.LogWarning("{this} failed to create meta data. error(s) - {errors}",
                    this, string.Join(", ", metaData.Reasons.Select(r => r.Message)));

                return Result.Fail(metaData.Errors);
            }

            var user = User.Create(metaData.Value);

            if (user.IsFailed)
            {
                _logger.LogWarning("{this} failed to register user. error(s) - '{errors}'", 
                    this, string.Join(", ", user.Reasons.Select(r => r.Message)));

                return Result.Fail(user.Errors);
            }

            var register = await _userRepository.RegisterUser(user.Value);

            if(register.IsFailed)
            {
                _logger.LogWarning("{this} (infrastructure) failed to register. error(s) - '{errors}'",
                    this, string.Join(", ", register.Reasons.Select(r => r.Message)));

                return Result.Fail(register.Errors);
            }

            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogDebug("{this} registered new user successfully",
                this);

            return user.Value;
        }

        public override string ToString() => Name;

    }

}
