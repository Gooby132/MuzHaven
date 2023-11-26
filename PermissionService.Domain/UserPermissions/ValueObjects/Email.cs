using FluentResults;
using PermissionService.Domain.UserPermissions.Errors;
using System.ComponentModel.DataAnnotations;

namespace PermissionService.Domain.UserPermissions.ValueObjects;

public class Email
{
    public string Raw { get; init; }

    private Email() { }

    public static Result<Email> Create(string email)
    {
        if (!(new EmailAddressAttribute().IsValid(email)))
            return EmailErrors.EmailIsInvalid();

        return new Email
        {
            Raw = email,
        };
    }

    public static bool operator ==(Email a, string b) => a.Raw == b;
    public static bool operator !=(Email a, string b) => a.Raw != b;
}
