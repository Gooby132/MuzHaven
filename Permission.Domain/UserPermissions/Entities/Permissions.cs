using Ardalis.SmartEnum;

namespace PermissionService.Domain.UserPermissions.Entities;

public class Permissions : SmartEnum<Permissions>
{

    public static readonly Permissions Guest = new Permissions("Guest", 1);
    public static readonly Permissions Normal = new Permissions("Normal", 2);
    public static readonly Permissions Verified = new Permissions("Verified", 3);

    private Permissions(string name, int value) : base(name, value) { }

}
