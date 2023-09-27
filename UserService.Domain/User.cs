using DomainSeed;
using FluentResults;
using UserService.Domain.ValueObjects;

namespace UserService.Domain;

public class User : Entity<Guid>
{
    public PersonMetaData MetaData { get; }

    private User() { }

    private User(PersonMetaData metaData) 
    { 
        MetaData = metaData; 
    }

    public static Result<User> Create(PersonMetaData metaData)
    {
        return new User(metaData);
    }
}
