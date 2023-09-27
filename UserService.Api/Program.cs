using MediatR;
using UserService.Application.DependencyInjection;
using UserService.Persistence.DependencyInjection;
using UserService.Application.Users.Command;
using UserService.Application.Users.Queries;
using UserService.Contracts.Requests;
using UserService.Domain.Errors;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureApplication(builder.Configuration);
builder.Services.ConfigurePersistence(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/register-user", async (RegisterUserRequest request, IMediator mediator) =>
{
    var res = await mediator.Send(new RegisterUserCommand.Command
    {
        Name = request.Name,
        Bio = request.Bio,
        Image = request.Image,
    });

    if (res.IsFailed)
    {
        if (res.HasError<InvalidNameError>())
            return Results.BadRequest(res);

        return Results.Problem("failure");
    }

    return Results.CreatedAtRoute("GetUserById", new { res.Value.Id });
})
.WithName("RegisterUser");

app.MapGet("/get-user-by-id/{id}", async (Guid id,[FromServices] IMediator mediator) =>
{
    var res = await mediator.Send(new GetUserByIdQuery.Query
    {
        Id = id,
    });

    if (res.IsFailed)
    {
        if(res.HasError<NotFoundError>())
            return Results.NotFound(id);

        return Results.Problem();
    }

    return Results.Ok(res.Value);
})
.WithName("GetUserById");

app.Run();
