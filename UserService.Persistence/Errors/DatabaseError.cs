using FluentResults;

namespace UserService.Persistence.Errors;

public class DatabaseError : Error
{
    public DatabaseError(Exception e) : base($"Database error occurred. message - '{e.Message}'")
    {
        Exception = e;
    }

    public Exception Exception { get; }
}
