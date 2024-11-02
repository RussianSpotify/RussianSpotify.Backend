using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace RussianSpotify.API.WEB.Configurations;

/// <summary>
/// Конфигурация аутентификации
/// </summary>
public static class ConfigureAuthenticationExtension
{
    /// <summary>
    /// Добавление Аутентификации с настройкой JwtBearer
    /// </summary>
    /// <param name="services">Коллекция сервисов билдера</param>
    /// <param name="configuration">конфигурация(appsettings.json)</param>
    /// <returns>AuthenticationBuilder</returns>
    public static AuthenticationBuilder AddAuthenticationWithJwtAndExternalServices(this IServiceCollection services,
        IConfiguration configuration) =>
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = configuration["JWT:ValidAudience"],
                ValidateLifetime = true,
                ValidIssuer = configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!))
            };
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    if (!string.IsNullOrEmpty(accessToken)
                        && !string.IsNullOrEmpty(context.Request.Path.Value)
                        && context.Request.Path.Value.Contains("/chat-hub"))
                        context.Token = accessToken;

                    return Task.CompletedTask;
                }
            };
        }).AddGoogle(config =>
        {
            config.ClientId = configuration["Authentication:Google:ClientId"]!;
            config.ClientSecret = configuration["Authentication:Google:ClientSecret"]!;
        });
}