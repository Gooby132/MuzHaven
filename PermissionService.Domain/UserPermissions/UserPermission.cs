using FluentResults;
using PermissionService.Domain.UserPermissions.ValueObjects;
using PermissionService.Domain.UserPermissions.Errors;
using DomainSeed.ValueObjects.Internet;

namespace PermissionService.Domain.UserPermissions;

public class UserPermission
{

    public Email Email { get; init; } 
    public Password Password { get; init; }
    public Permissions Permission { get; private set; }

    private UserPermission() { }

    public static Result<UserPermission> Create(string emailText, string passwordText)
    {

        var password = Password.Create(passwordText);
        var email = Email.Create(emailText);

        if (password.IsFailed ||
            email.IsFailed)
            return Result
                .Fail(password.Errors)
                .WithErrors(email.Errors);

        return new UserPermission()
        {
            Email = email.Value,
            Password = password.Value,
            Permission = Permissions.Guest,
        };
    }

    public Result Authorize(string password)
    {
        if (Password != password)
            return Result.Fail(Unauthorized.BadPassword());

        return Result.Ok();
    }

    public Result Verify()
    {
        if (Permission != Permissions.Guest)
            return InvalidOperationError.InvalidTransitionOfPermission();

        Permission = Permissions.Normal;

        return Result.Ok();
    }

    public Result Elevate()
    {
        if (Permission != Permissions.Normal)
            return InvalidOperationError.InvalidTransitionOfPermission();

        Permission = Permissions.Verified;

        return Result.Ok();
    }
}
