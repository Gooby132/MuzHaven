namespace PermissionService.Contracts.Requests;

public class CreatePermissionRequest
{
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public UserPermission Permission { get; set; }

    public enum UserPermission {
        Guest,
        Reader,
        Commenter,
        Contributer,
    }
}
