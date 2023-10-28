using DomainSeed;
using FluentResults;

namespace ProjectService.Domain.ValueObjects;

public class MetaData : ValueObject
{

    public string Name { get; init; }

    private MetaData() { }

    public static Result<MetaData> Create(string name)
    {
        return new MetaData() 
        { 
            Name = name 
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}
