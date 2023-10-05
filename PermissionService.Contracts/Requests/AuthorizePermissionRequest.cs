namespace PermissionService.Contracts.Requests;

public class AuthorizePermissionRequest
{
    public string Permission { get; set; }
    public string AutorizationPassword { get; set; }
}
