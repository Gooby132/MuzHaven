namespace DomainSeed;

public abstract class Entity<EntityId>
{
    public EntityId Id { get; set; }
}