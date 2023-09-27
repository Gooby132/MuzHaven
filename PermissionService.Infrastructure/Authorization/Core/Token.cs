namespace PermissionService.Infrastructure.Authorization.Core;

public class Token
{

    public string RawToken { get; }

    public Token(string rawToken)
    {
        RawToken = rawToken;
    }
}
