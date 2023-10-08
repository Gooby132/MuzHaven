using ApiService.Application.Users.Commands;
using ApiService.Application.Users.Queries;
using ApiService.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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

    #endregion

    public string Name { get; set; }

    public UsersController(
        ILogger<UsersController> logger,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RegisterResponse))]
    [HttpPost("register-user")]
    public async Task<IActionResult> Registration(
        RegistrationRequest request,
        CancellationToken token = default)
    {

        var res = await _mediator.Send(new RegisterUser.Command
        {
            Bio = request.Bio,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            StageName = request.StageName,
        });

        if (res.IsFailed)
        {
            // defined errors

            _logger.LogTrace("{this} user failed to be registered. errors - '{errors}'",
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
                    Email = res.Value.MetaData.Email,
                }
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
        var user = await _mediator.Send(new GetUserById.Query
        {
            Id = request.Id,
        }, token);

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
                Email = user.Value.MetaData.Email,
            }
        });
    }

    public override string ToString() => Name;

}
