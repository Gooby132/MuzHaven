using Microsoft.AspNetCore.Mvc;
using PermissionService.Contracts.Requests;
using PermissionService.Infrastructure.Authorization.Abstracts;
using PermissionService.Infrastructure.Authorization.JwtProvider.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureJwtProvider(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/cached-permission", () =>
{

})
.WithName("cached-permissions");

app.MapPost("/create-permission", (CreateContextRequest request, [FromServices] IAuthorizationTokenProvider provider) =>
{
    switch (request.Permission)
    {
        case CreateContextRequest.Contexts.Guest:
            return Results.Ok(provider.CreateGuestToken(request.Permission));
        case CreateContextRequest.Contexts.Reader:
            return Results.Ok(provider.CreateReaderToken(request.Permission, request.ContextId));
        case CreateContextRequest.Contexts.Commenter:
            return Results.Ok(provider.CreateCommenterToken(request.Permission, request.ContextId));
        case CreateContextRequest.Contexts.Contributer:
            return Results.Ok(provider.CreateContributerToken(request.Permission, request.ContextId));
        default:
            return Results.BadRequest();
    }
});

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}