using Ardalis.SmartEnum;

namespace PermissionService.Domain.UserPermissions.ValueObjects;

public class Permissions : SmartEnum<Permissions>
{

    public static readonly Permissions Guest = new Permissions("Guest", 1, "1");
    public static readonly Permissions Normal = new Permissions("Normal", 2, "2");
    public static readonly Permissions Verified = new Permissions("Verified", 3, "3");
    public static readonly Permissions Admin = new Permissions("Admin", 4, "4");

    public string Type { get; }

    private Permissions(string name, int value, string type) : base(name, value) => Type = type;

}
