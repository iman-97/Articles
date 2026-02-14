using FileStorage.MOngoGridFS;

namespace Submission.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddMemoryCache()                   // Basic Caching
            .AddEndpointsApiExplorer()          // Minimal Api Docs
            .AddSwaggerGen();                   // Swagger Setup

        services.AddMongoFileStorage(configuration);

        return services;
    }
}
