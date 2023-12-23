using ApiService.Application.Projects.Commands;
using ApiService.Application.Users.Commands;
using ApiService.Application.Users.Queries;
using DomainSeed;
using DomainSeed.CommonErrors;
using DomainSeed.ValueObjects.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PermissionService.Contracts.Requests;
using PermissionService.Domain.UserPermissions.Errors;
using PermissionService.Infrastructure.Authorization.Abstracts;
using ProjectService.Contracts.Requests;
using ProjectService.Contracts.Responses;
using UserService.Contracts.Requests;
using UserService.Contracts.Responses;
using UserService.Domain.Errors;

namespace ApiService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{

    #region Fields

    private readonly ILogger<UsersController> _logger;
    private readonly IMediator _mediator;
    private readonly IPermissionTokenProvider _tokenProvider;

    #endregion

    public string Name { get; set; }

    public UsersController(
        ILogger<UsersController> logger,
        IMediator mediator,
        IPermissionTokenProvider tokenProvider)
    {
        _logger = logger;
        _mediator = mediator;
        _tokenProvider = tokenProvider;
    }

    [Authorize(AuthenticationSchemes = IPermissionTokenProvider.PermissionSchemeName)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateProjectResponse))]
    [HttpPost("create-project")]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request, CancellationToken token = default)
    {
        var id = User.Claims.FirstOrDefault(claim => claim.Type == IPermissionTokenProvider.UserIdClaim)?.Value;
        var firstName = User.Claims.FirstOrDefault(claim => claim.Type == IPermissionTokenProvider.FirstNameClaim)?.Value;
        var lastName = User.Claims.FirstOrDefault(claim => claim.Type == IPermissionTokenProvider.LastNameClaim)?.Value;
        var email = User.Claims.FirstOrDefault(claim => claim.Type == IPermissionTokenProvider.EmailClaim)?.Value;
        var stageName = User.Claims.FirstOrDefault(claim => claim.Type == IPermissionTokenProvider.StageClaim)?.Value;

        var person = UserService.Domain.ValueObjects.PersonMetaData.Create(firstName, lastName, email);
        var artist = UserService.Domain.ValueObjects.ArtistDescription.Create(stageName, null);

        var user = UserService.Domain.User.Login(id, person.Value, artist.Value);

        if (user.IsFailed)
        {
            _logger.LogError("{this} failed to create project as user cannot be constructed. errors(s) - '{errors}'",
                this, string.Join(", ", user.Errors.Select(e => e.Message)));

            return Problem();
        }

        var project = user.Value.CreateNewProject(
                request.Project.Title,
                request.Project.Album,
                request.Project.Description,
                request.Project.ReleaseInUtc,
                request.Project.BeatsPerMinute,
                request.Project.MusicalProfile?.Key,
                request.Project.MusicalProfile?.Scale);

        if (project.IsFailed)
        {
            if (project.HasError<ErrorBase>())
                return BadRequest(project.Errors.Select(r => new
                {
                    r.Message,
                    Code = (r as ErrorBase)?.ErrorCode,
                    Group = (r as ErrorBase)?.GroupCode,
                }));
        }

        var result = await _mediator.Send(new CreateProjectCommand.Command(project.Value), token);

        return CreatedAtAction(
          nameof(ProjectsController.GetById),
          "Projects",
          new { result.Value.Id },
          new CreateProjectResponse
          {
              Project = new ProjectService.Contracts.Dtos.CompleteProjectDto
              {
                  Id = result.Value.Id,
                  Title = result.Value.Title.Text,
                  Album = result.Value.Album,
                  BeatsPerMinute = result.Value.BeatsPerMinute,
                  CreatedInUtc = result.Value.CreatedInUtc.ToString("O"),
                  Description = result.Value.Description.Text,
                  ReleaseInUtc = result.Value.ReleaseInUtc?.ToString("O"),
                  MusicalProfile = result.Value.MusicalProfile is not null ? new ProjectService.Contracts.Dtos.MusicalProfileDto
                  {
                      Scale = (int)result.Value.MusicalProfile.Scale,
                      Key = (int)result.Value.MusicalProfile.Key,
                  } : null,
              }
          });
    }

    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RegisterResponse))]
    [HttpPost("register-user")]
    public async Task<IActionResult> Registration(
        [FromBody] RegisterRequest request,
        CancellationToken token = default)
    {

        var res = await _mediator.Send(new RegisterUser.Command
        {
            Bio = request.Bio,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            StageName = request.StageName,
            Password = request.Password,
        });

        if (res.IsFailed)
        {
            // defined errors

            if (res.HasError<ErrorBase>())
                return BadRequest(res.Errors.Select(r => new
                {
                    r.Message,
                    Code = (r as ErrorBase)?.ErrorCode,
                    Group = (r as ErrorBase)?.GroupCode,
                }));

            _logger.LogError("{this} user failed to be registered. errors - '{errors}'",
                this, string.Join(", ", res.Reasons.Select(r => r.Message)));

            return Problem(); // default undefined error
        }

        return CreatedAtAction(
            nameof(GetUserById),
            new { res.Value.Id },
            new RegisterResponse
            {
                User = new UserService.Contracts.Dtos.UserDto
                {
                    Id = res.Value.Id,
                    StageName = res.Value.ArtistDescription.StageName,
                    Bio = res.Value.ArtistDescription.Bio,
                    FirstName = res.Value.MetaData.FirstName,
                    LastName = res.Value.MetaData.LastName,
                    Email = res.Value.MetaData.Email.Raw,
                },
                Token = _tokenProvider.CreateGuestToken(
                    res.Value.Id.ToString(),
                    res.Value.MetaData.FirstName,
                    res.Value.MetaData.LastName,
                    res.Value.MetaData.Email.Raw,
                    res.Value.ArtistDescription.StageName
                    ).RawToken,
            });
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginUserResponse))]
    [HttpPost("login-user")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        CancellationToken token = default)
    {

        var login = await _mediator.Send(new LoginUserByEmail.Command(request.Email, request.Password), token);

        if (login.IsFailed)
        {
            if (login.HasError<NotFoundError>())
                return NotFound(request.Email);

            if (login.HasError<Unauthorized>())
                return Unauthorized(request.Email);

            _logger.LogError("{this} failed to login user with email - '{email}'. error(s) - '{errors}'",
                this, request.Email, string.Join(", ", login.Reasons.Select(r => r.Message)));

            return Problem();
        }

        return Ok(new LoginUserResponse
        {
            User = new UserService.Contracts.Dtos.UserDto
            {
                Id = login.Value.User.Id,
                Email = login.Value.User.MetaData.Email.Raw,
                FirstName = login.Value.User.MetaData.FirstName,
                LastName = login.Value.User.MetaData.LastName,
                Bio = login.Value.User.ArtistDescription.Bio,
                StageName = login.Value.User.ArtistDescription.StageName
            },
            Token = _tokenProvider.CreateGuestToken(
                    request.Email,
                    login.Value.User.MetaData.FirstName,
                    login.Value.User.MetaData.LastName,
                    login.Value.User.MetaData.Email.Raw,
                    login.Value.User.ArtistDescription.StageName
                    ).RawToken
        });
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("get-user")]
    public async Task<IActionResult> GetUserById(
        [FromQuery] GetUserByIdRequest request,
        CancellationToken token = default)
    {
        var user = await _mediator.Send(new GetUserById.Query(
            request.Id
            ), token);

        if (user.IsFailed)
        {
            if (user.HasError<NotFoundError>())
                return NotFound(request.Id);

            return Problem();
        }

        return Ok(new GetUserResponse
        {
            User = new UserService.Contracts.Dtos.UserDto
            {
                Id = request.Id,
                StageName = user.Value.ArtistDescription.StageName,
                Bio = user.Value.ArtistDescription.Bio,
                FirstName = user.Value.MetaData.FirstName,
                LastName = user.Value.MetaData.LastName,
                Email = user.Value.MetaData.Email.Raw,
            }
        });
    }

    public override string ToString() => Name;

}
