using System.Collections.Generic;

namespace DomainSeed;

public abstract class Aggregate<AggregateId> : Entity<AggregateId>
{

    public abstract DomainEvent? DequeueDomainEvent();

}
