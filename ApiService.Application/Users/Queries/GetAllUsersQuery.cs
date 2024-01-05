using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using UserService.Domain;
using UserService.Domain.Repositories;

namespace ApiService.Application.Users.Queries;

public static class GetAllUsersQuery
{

    public record Query : IRequest<Result<IEnumerable<User>>>;

    internal class Handler : IRequestHandler<Query, Result<IEnumerable<User>>>
	{
        private readonly ILogger<Handler> _logger;
        private readonly IUserRepository _repository;

        public Handler(ILogger<Handler> logger, IUserRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public Task<Result<IEnumerable<User>>> Handle(Query request, CancellationToken cancellationToken)
        {
            return _repository.GetAll(cancellationToken);
        }
    }

}
