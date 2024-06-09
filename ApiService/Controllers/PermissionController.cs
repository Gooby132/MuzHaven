using ApiService.Application.Permissions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PermissionService.Contracts.Requests;
using PermissionService.Domain.UserPermissions.Errors;
using PermissionService.Infrastructure.Authorization.Abstracts;

namespace ApiService.Controllers;


/// <summary>
/// REST controller for remote autherization controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PermissionController : ControllerBase
{
    private readonly ILogger<PermissionController> _logger;
    private readonly IPermissionTokenProvider _tokenProvider;
    private readonly IMediator _mediator;

    public string Name { get; set; } = nameof(PermissionController);

    public PermissionController(
        ILogger<PermissionController> logger,
        IPermissionTokenProvider tokenProvider,
        IMediator mediator)
    {
        _logger = logger;
        _tokenProvider = tokenProvider;
        _mediator = mediator;
    }

    /// <summary>
    /// Autherizes a user
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken token = default)
    {

        var res = await _mediator.Send(new GetPermissionByUserIdQuery.Query
        {
            Email = request.Email,
            Password = request.Password
        });

        if (res.IsFailed)
        {
            if (res.HasError<Unauthorized>())
                return Unauthorized();

            return Problem();
        }

        return Problem();
    }

    //    [Authorize()]
    //    [HttpPatch("refresh-token")]
    //    public IActionResult RefreshToken()
    //    {

    //        var type = User.Claims
    //            .FirstOrDefault(claim => claim.Type == IPermissionTokenProvider.PermissionTypeClaim);
    //        var userId = User.Claims
    //            .FirstOrDefault(claim => claim.Type == IPermissionTokenProvider.UserIdClaim);

    //        if (type is null)
    //        {
    //            _logger.LogError("{this} type received was null.",
    //                this);

    //            return BadRequest(nameof(type));
    //        }

    //        if (userId is null)
    //        {
    //            _logger.LogError("{this} userId received was null.",
    //                this);

    //            return BadRequest(nameof(type));
    //        }

    //        if (!Permissions.TryFromName(type.Value, true, out var permission))
    //        {
    //            _logger.LogError("{this} type received was malformed.",
    //                this);

    //            return BadRequest(nameof(type));
    //        }

    //        switch (permission.Name)
    //        {
    //            case nameof(Permissions.Guest):
    //                return Ok(_tokenProvider.CreateGuestToken(userId.Value).RawToken);
    //            case nameof(Permissions.Normal):
    //            case nameof(Permissions.Verified):
    //            default:
    //                break;
    //        }

    //        return Problem();
    //    }

    //    public override string ToString() => Name;

}
