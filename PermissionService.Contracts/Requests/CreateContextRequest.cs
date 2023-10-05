namespace PermissionService.Contracts.Requests;

public class CreateContextRequest
{
    public Guid Permitted { get; set; }
    public Guid ContextId { get; set; }
    public string Password { get; set; }
    public Contexts Context { get; set; }

    public enum Contexts {
        Reader,
        Commenter,
        Contributer,
    }
}
