using DomainSeed.ValueObjects.Internet.Errors;
using FluentResults;
using System.ComponentModel.DataAnnotations;

namespace DomainSeed.ValueObjects.Internet;

public class Email
{
    public string Raw { get; init; }

    private Email() { }

    public static Result<Email> Create(string? email)
    {
        if(string.IsNullOrEmpty(email))
            return EmailErrors.EmailIsInvalid();

        if (!new EmailAddressAttribute().IsValid(email))
            return EmailErrors.EmailIsInvalid();

        return new Email
        {
            Raw = email,
        };
    }

    public static bool operator ==(Email a, string b) => a.Raw == b;
    public static bool operator !=(Email a, string b) => a.Raw != b;
}
