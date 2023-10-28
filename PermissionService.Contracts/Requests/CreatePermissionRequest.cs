namespace PermissionService.Contracts.Requests;

public class CreatePermissionRequest
{
    public string ContextId { get; set; }
    public string Password { get; set; }
}
