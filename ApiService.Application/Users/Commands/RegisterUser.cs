﻿using ApiService.Application.Dispatcher;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using PermissionService.Domain.UserPermissions;
using PermissionService.Domain.UserPermissions.Repositories;
using PermissionService.Domain.UserPermissions.UnitOfWork;
using UserService.Domain;
using UserService.Domain.Repositories;
using UserService.Domain.ValueObjects;
using UserService.Persistence.Context;

namespace ApiService.Application.Users.Commands;

public static class RegisterUser
{

    public class Command : IRequest<Result<User>>
    {
        public string StageName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public string Password { get; set; }
    }

    internal class Handler : IRequestHandler<Command, Result<User>>
    {
        private readonly ILogger<Handler> _logger;
        private readonly IUserRepository _repository;
        private readonly IUserUnitOfWork _userUnitOfWork;
        private readonly IUserPermissionRepository _permissionRepository;
        private readonly IUserPermissionUnitOfWork _userPermissionUnitOfWork;
        private readonly IMediator _mediator;

        public Handler(
            ILogger<Handler> logger,
            IUserRepository repository,
            IUserUnitOfWork userUnitOfWork,
            IUserPermissionRepository permissionRepository,
            IUserPermissionUnitOfWork userPermissionUnitOfWork,
            IMediator mediator)
        {
            _logger = logger;
            _repository = repository;
            _userUnitOfWork = userUnitOfWork;
            _permissionRepository = permissionRepository;
            _userPermissionUnitOfWork = userPermissionUnitOfWork;
            _mediator = mediator;
        }
        public async Task<Result<User>> Handle(Command request, CancellationToken cancellationToken)
        {

            _logger.LogTrace("{this} register user was requested",
                this);

            // create permission
            var permission = UserPermission.Create(request.Email, request.Password);

            if (permission.IsFailed)
            {
                _logger.LogTrace("{this} bad request creating permission. error(s) - '{errors}'",
                    this, string.Join(", ", permission.Reasons.Select(r => r.Message)));

                return Result.Fail(permission.Errors);
            }

            var permit = await _permissionRepository.Permit(permission.Value);

            if(permit.IsFailed)
            {
                _logger.LogError("{this} (infrastructure) failed to permit user. error(s) - '{errors}'",
                    this, string.Join(", ", permit.Reasons.Select(r => r.Message)));

                return Result.Fail(permission.Errors);
            }

            var persistPermit = await _userPermissionUnitOfWork.CommitAsync();

            if (persistPermit.IsFailed)
            {
                _logger.LogError("{this} (infrastructure) failed to persist permit user. error(s) - '{errors}'",
                    this, string.Join(", ", persistPermit.Reasons.Select(r => r.Message)));

                return Result.Fail(persistPermit.Errors);
            }

            // create user
            var person = PersonMetaData.Create(
                request.FirstName, 
                request.LastName, 
                request.Email);

            var artists = ArtistDescription.Create(
                request.StageName,
                request.Bio);

            if(person.IsFailed || artists.IsFailed)
            {
                if (artists.IsFailed)
                    _logger.LogTrace("{this} bad request creating artists creation. error(s) - '{errors}'",
                        this, string.Join(", ", person.Reasons.Select(r => r.Message)));


                if(person.IsFailed)
                    _logger.LogTrace("{this} bad request creating person meta data. error(s) - '{errors}'",
                        this, string.Join(", ", person.Reasons.Select(r => r.Message)));

                return Result
                    .Fail(person.Errors)
                    .WithErrors(artists.Errors);
            }

            var user = User.Create(person.Value, artists.Value);

            if(user.IsFailed)
            {
                _logger.LogTrace("{this} bad request creating user. error(s) - '{errors}'",
                    this, string.Join(", ", user.Reasons.Select(r => r.Message)));

                return Result.Fail(user.Errors);
            }

            var register = await _repository.RegisterUser(user.Value, cancellationToken);

            if (register.IsFailed)
            {
                _logger.LogError("{this} (infrastructure) failed to register user. error(s) - '{errors}'",
                    this, string.Join(", ", register.Reasons.Select(r => r.Message)));

                return Result.Fail(register.Errors);
            }

            var persist = await _userUnitOfWork.CommitAsync(cancellationToken);

            if(persist.IsFailed)
            {
                _logger.LogError("{this} (infrastructure) failed to persist user. error(s) - '{errors}'",
                    this, string.Join(", ", persist.Reasons.Select(r => r.Message)));

                return Result.Fail(persist.Errors);
            }

            _logger.LogDebug("{this} register user was successful",
                this);

            await _mediator.DispatchDomainEvents(user.Value, cancellationToken);

            return Result.Ok(user.Value);
        }
    }

}
