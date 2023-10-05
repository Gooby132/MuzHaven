using ApiService.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
using PermissionService.Contracts.Requests;
using PermissionServiceClient.Abstractions;
using UserService.Contracts.Requests;
using UserServiceClient.Abstractions;

namespace ApiService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{

    #region Fields
    
    private readonly ILogger<UsersController> _logger;
    private readonly IPermissionServiceClient _permissionClient;
    private readonly IUserServiceClient _userClient;

    #endregion

    public string Name { get; set; }

    public UsersController(
        ILogger<UsersController> logger, 
        IPermissionServiceClient permissionClient,
        IUserServiceClient userClient)
    {
        _logger = logger;
        _permissionClient = permissionClient;
        _userClient = userClient;
    }

    public async Task<IActionResult> Registration(
        RegistrationRequest request, 
        CancellationToken token = default)
    {
        var permission = await _permissionClient.Register(new CreatePermissionRequest
        {
            ContextId = request.StageName,
            Password = request.Password
        }, token); 

        if (permission.IsFailed)
        {
            _logger.LogError("{this} failed to create permission for user. error(s) - '{errors}'",
                this, string.Join(", ", permission.Reasons.Select(r => r.Message)));

            return Problem();
        }

        var user = await _userClient.Register(new RegisterRequest
        {
            StageName = request.StageName,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Bio = request.Bio,
        });

        if (user.IsFailed)
        {
            _logger.LogError("{this} failed to register user. error(s) - '{errors}'",
                this, string.Join(",", user.Reasons.Select(r => r.Message)));

            return Problem();
        }

        return Ok(token);
    }

    public override string ToString() => Name;

}
