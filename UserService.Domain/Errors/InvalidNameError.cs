using FluentResults;

namespace UserService.Domain.Errors;

public class InvalidNameError : Error
{

    public ErrorCodes ErrorCode { get; }

    public InvalidNameError(ErrorCodes errorCode) : base("Invalid name") 
    { 
        ErrorCode = errorCode;
    }
    public InvalidNameError(string message, ErrorCodes errorCode) : 
        base($"Invalid name - {message}") 
    {
        ErrorCode = errorCode;
    }

    public enum ErrorCodes
    {
        GeneralCode = 1,
        NameTooShortCode = 2,
        NameTooLongCode = 3,
    }

}
