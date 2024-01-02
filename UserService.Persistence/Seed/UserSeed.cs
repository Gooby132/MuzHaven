using Bogus;
using Microsoft.Extensions.DependencyInjection;
using UserService.Domain;
using UserService.Domain.ValueObjects;
using UserService.Persistence.Context;

namespace UserService.Persistence.Seed;

public static class UserSeed
{
    public static IServiceProvider SeedUsers(this IServiceProvider provider, int count = 30, CancellationToken token = default)
    {

        var scope = provider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<UserContext>();

        List<User> users = new List<User>();

        for (int i = 0; i < count; i++)
        {
            var faker = new Faker();

            users.Add(User.Create(
                PersonMetaData.Create(
                    faker.Person.FirstName,
                    faker.Person.LastName,
                    faker.Internet.Email()
                    ).Value,
                ArtistDescription.Create(
                    faker.Person.UserName,
                    faker.Lorem.Paragraph()
                    ).Value
                ).Value);
        }

        context.AddRange(users, token);

        context.Dispose();
        scope.Dispose();

        return provider;
    }

}
