using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StemService.Domain.Services;
using StemService.Infrastructure.FileAuthorizer.JwtAuthorizer.Core;
using StemService.Infrastructure.FileAuthorizer.JwtAuthorizer.Options;
using System.Text;

namespace StemService.Infrastructure.FileAuthorizer.JwtAuthorizer.DependencyInjection;

public static class Configure
{

    public const string Key = "JwtTokenProvider";

    public static IServiceCollection ConfigureJwtProvider(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        configuration = configuration.GetRequiredSection(Key);

        services.AddOptions<JwtProviderOptions>()
            .Bind(configuration.GetRequiredSection(JwtProviderOptions.OptionsKey))
            .ValidateDataAnnotations();

        services
            .AddAuthentication(IFileAuthorizer.StemSchemeName)
            .AddJwtBearer(IFileAuthorizer.StemSchemeName, options =>
            {
                string key = configuration["Options:Key"];
                string issuer = configuration["Options:Issuer"];
                string audience = configuration["Options:Audience"];

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Request.Headers.TryGetValue(IFileAuthorizer.StemHeaderName, out var res);
                        context.Token = res;

                        return Task.CompletedTask;
                    } 
                };

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Options:Issuer"],
                    ValidAudience = configuration["Options:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };
            });

        services.AddTransient<IFileAuthorizer, JwtTokenProvider>();

        return services;
    }

}
