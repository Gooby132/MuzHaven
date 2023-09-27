using FluentResults;

namespace UserService.Domain.Errors;

public class InvalidBioError : Error
{
    public ErrorCodes ErrorCode { get; }

    public InvalidBioError(ErrorCodes errorCode) : base("Invalid bio") 
    { 
        ErrorCode = errorCode;
    }
    public InvalidBioError(string message, ErrorCodes errorCode) : 
        base($"Invalid bio - {message}") 
    {
        ErrorCode = errorCode;
    }

    public enum ErrorCodes
    {
        GeneralCode = 1,
        BioTooShortCode = 2,
        BioTooLongCode = 3,
    }

}
