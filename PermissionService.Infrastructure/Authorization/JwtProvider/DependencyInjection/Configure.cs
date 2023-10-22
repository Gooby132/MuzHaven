using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PermissionService.Infrastructure.Authorization.Abstracts;
using PermissionService.Infrastructure.Authorization.JwtProvider.Core;
using PermissionService.Infrastructure.Authorization.JwtProvider.Options;
using System.Text;

namespace PermissionService.Infrastructure.Authorization.JwtProvider.DependencyInjection;

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
            .AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            string key = configuration["Options:Key"];
            string issuer = configuration["Options:Issuer"];
            string audience = configuration["Options:Audience"];

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

        services.AddTransient<IAuthorizationTokenProvider, JwtTokenProvider>();

        return services;
    }

}
