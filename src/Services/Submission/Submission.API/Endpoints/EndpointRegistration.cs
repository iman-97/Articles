namespace Submission.API.Endpoints;

public static class EndpointRegistration
{
    public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder builder)
    {
        CreateArticleEndpoint.Map(builder);

        return builder;
    }
}
