namespace DomainSeed.ValueObjects.Auth;

public class Token
{

    public string RawToken { get; }

    public Token(string rawToken)
    {
        RawToken = rawToken;
    }
}
