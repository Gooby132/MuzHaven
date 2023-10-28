using Ardalis.SmartEnum;

namespace PermissionService.Domain.ProjectPermissions.ValueObjects;

public class Permissions : SmartEnum<Permissions>
{

    public static readonly Permissions Creator = new Permissions("Creator", 1);
    public static readonly Permissions Contributer = new Permissions("Contributer", 2);
    public static readonly Permissions Commenter = new Permissions("Commenter", 3);
    public static readonly Permissions Reader = new Permissions("Reader", 4);

    private Permissions(string name, int value) : base(name, value) {  }
}
