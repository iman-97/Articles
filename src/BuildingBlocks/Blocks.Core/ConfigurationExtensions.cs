using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blocks.Core;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddAndValidateOptins<TOptions>(this IServiceCollection services, IConfiguration config)
        where TOptions : class
    {
        var section = config.GetSection(typeof(TOptions).Name);


        if (section.Exists() == false)
            throw new InvalidOperationException($"section {section.Key} is missing");

        services
            .AddOptions<TOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }

    public static T GetSectionByTypeName<T>(this IConfiguration configuration)
    {
        var sectionName = typeof(T).Name;
        var section = configuration.GetSection(sectionName).Get<T>()!;

        return Guard.AgainstNull(section, sectionName);
    }

    public static string GetConnectionStringOrThrow(this IConfiguration configuration, string name)
    {
        var value = configuration.GetConnectionString(name);

        if (string.IsNullOrEmpty(value))
            throw new InvalidOperationException($"Connection string {name} is missing or empty");

        return value!;
    }
}
