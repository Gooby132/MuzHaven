using ApiService.Application.Dispatcher.Errors;
using DomainSeed;
using FluentResults;
using MediatR;

namespace ApiService.Application.Dispatcher;

public static class DomainService
{

    public static async Task<Result> DispatchDomainEvents<AggregateId>(this IMediator mediator, Aggregate<AggregateId> aggregate, CancellationToken token = default)
    {

        var errors = new List<Error>();

        DomainEvent? domainEvent;
        while ((domainEvent = aggregate.DequeueDomainEvent()) is not null)
        {
            try
            {
                await mediator.Publish(domainEvent);
            }
            catch (Exception exception)
            {
                errors.Add(new UndefinedDomainEventExceptionError(domainEvent, exception));
            }
        }

        return errors.Any() ? Result.Fail(errors) : Result.Ok();
    }

}
