using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace RussianSpotify.API.Shared.Extensions.ConfigurationExtensions;

/// <summary>
/// Конфигурация сваггера
/// </summary>
public static class ConfigureSwaggerExtension
{
    /// <summary>
    /// Добавление SwaggerGen и настройка его Аутентификации и XML-документации
    /// </summary>
    /// <param name="services">Сервисы билдера</param>
    /// <param name="assembly">Assembly</param>
    /// <returns>IServiceCollection</returns>
    public static void AddSwaggerGenWithAuth(this IServiceCollection services, Assembly assembly) =>
        services.AddSwaggerGen(opt =>
        {
            var xmlFileName = $"{assembly.GetName().Name}.xml";
            opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));

            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Authorization token"
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
}