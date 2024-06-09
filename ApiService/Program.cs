using ApiService.Application.DependencyInjection;
using HashidsNet;
using ApiService.Seeds.Users;
using PermissionService.Infrastructure.Authorization.Abstracts;

const string DevelopmentCorsOrigins = "DevelopmentCorsOrigin";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<IHashids>(_ => new Hashids("benzine", 4));
builder.Services.ConfigureApiService(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy(DevelopmentCorsOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:3000")
        .AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(DevelopmentCorsOrigins);
    //app.Services.SeedUsersWithProjects(token: app.Lifetime.ApplicationStopping);
    var adminToken = app.Services.GetRequiredService<IPermissionTokenProvider>().CreateAdminToken();
}

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
