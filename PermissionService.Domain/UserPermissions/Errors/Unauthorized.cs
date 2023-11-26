using DomainSeed;
using FluentResults;

namespace PermissionService.Domain.UserPermissions.Errors;

public class Unauthorized : ErrorBase
{
    public const int UnauthorizedGroupError = 4;

    public const int BadPasswordErrorCode = 1;

    private Unauthorized(string message, int code) : base(message, code, UnauthorizedGroupError) { }

    public static Unauthorized BadPassword() =>
        new Unauthorized("Password given is wrong", BadPasswordErrorCode);

}
