using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = configuration["Options:Issuer"],
                ValidAudience = configuration["Options:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["Options:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
            };
        });

        services.AddTransient<IAuthorizationTokenProvider, JwtTokenProvider>();

        return services;
    }

}
