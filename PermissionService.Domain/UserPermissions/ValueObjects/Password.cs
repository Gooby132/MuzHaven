using DomainSeed;
using FluentResults;
using PermissionService.Domain.UserPermissions.Errors;

namespace PermissionService.Domain.UserPermissions.ValueObjects;

public class Password : ValueObject
{
    public const int MinimumLength = 8;

    public string Text { get; init; }
    
    private Password() { }

    public static Result<Password> Create(string password)
    {
        if(password.Length < MinimumLength)
            return PasswordErrors.PasswordTooShort();

        return new Password
        {
            Text = password,
        };
    }

    public static bool operator  ==(Password left, string right) => left.Text == right;
    public static bool operator  !=(Password left, string right) => left.Text != right;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Text;
    }
}
