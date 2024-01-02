using Bogus;
using Microsoft.Extensions.DependencyInjection;
using ProjectService.Domain.Context;
using ProjectService.Domain.Repositories;
using UserService.Domain;
using UserService.Domain.Repositories;
using UserService.Domain.ValueObjects;
using UserService.Persistence.Context;

namespace ApiService.Seeds.Users;

public static class Seed
{

    public static IServiceProvider SeedUsers(this IServiceProvider provider, int count = 30, CancellationToken token = default)
    {
        var uow = provider.GetRequiredService<IUserUnitOfWork>();
        var repository = provider.GetRequiredService<IUserRepository>();

        for (int i = 0; i < count; i++)
        {
            var faker = new Faker();
            var user = User.Create(
            PersonMetaData.Create(
                faker.Person.FirstName,
                faker.Person.LastName,
                faker.Internet.Email()
                ).Value,
            ArtistDescription.Create(
                faker.Person.UserName,
                faker.Lorem.Paragraph()
                ).Value
            ).Value;

            repository.RegisterUser(user);
        }

        uow.Dispose();

        return provider;
    }

    public static IServiceProvider SeedUsersWithProjects(this IServiceProvider provider, int count = 30, int projectsPerUser = 2, CancellationToken token = default)
    {
        var scope = provider.CreateScope();

        var userUow = scope.ServiceProvider.GetRequiredService<IUserUnitOfWork>();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

        var projectUow = scope.ServiceProvider.GetRequiredService<IProjectUnitOfWork>();
        var projectRepository = scope.ServiceProvider.GetRequiredService<IProjectRepository>();

        for (int i = 0; i < count; i++)
        {
            var faker = new Faker();
            var user = User.Create(
                PersonMetaData.Create(
                    faker.Person.FirstName,
                    faker.Person.LastName,
                    faker.Internet.Email()
                    ).Value,
                ArtistDescription.Create(
                    faker.Person.UserName,
                    faker.Lorem.Paragraph(1)
                    ).Value
            ).Value;

            userRepository.RegisterUser(user);

            for (int j = 0; j < Random.Shared.Next(0, projectsPerUser); j++)
            {
                var project = user.CreateNewProject(
                        faker.Vehicle.Manufacturer(),
                        faker.Vehicle.Model(),
                        faker.Lorem.Paragraph(1),
                        faker.Date.Between(DateTime.UtcNow.AddDays(1), new DateTime(2025, 1, 1)).ToString("O"),
                        faker.Random.Float(0, 999),
                        null,
                        null);

                projectRepository.CreateProject(project.Value);
            }
        }

        projectUow.Commit();
        projectUow.Dispose();

        userUow.Commit();
        userUow.Dispose();

        scope.Dispose();

        return provider;
    }

}
