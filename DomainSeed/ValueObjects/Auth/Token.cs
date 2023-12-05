namespace DomainSeed.ValueObjects.Auth;

public class Token
{

    public string RawToken { get; }
    public string Id { get; set; }

    public Token(string rawToken)
    {
        RawToken = rawToken;
    }
}
