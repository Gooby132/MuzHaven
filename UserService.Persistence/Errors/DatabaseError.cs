using FluentResults;

namespace UserService.Persistence.Errors;

public class DatabaseError : Error
{
    public DatabaseError(Exception e)
    {
        Exception = e;
    }

    public Exception Exception { get; }
}
