using DomainSeed;
using FluentResults;

namespace ApiService.Application.Dispatcher.Errors;

public class UndefinedDomainEventExceptionError : Error
{
    public UndefinedDomainEventExceptionError(DomainEvent domainEvent, Exception exception)
    {
        Exception = exception;
    }

    public Exception Exception { get; }
}
