using DomainSeed;
using FluentResults;

namespace PermissionService.Domain.UserPermissions.Errors;

public class Unauthorized : ErrorBase
{
    public const int BadPasswordErrorCode = 1;

    private Unauthorized(string message, int code) : base(message, code) { }

    public static Unauthorized BadPassword() =>
        new Unauthorized("Password given is wrong", BadPasswordErrorCode);

}
