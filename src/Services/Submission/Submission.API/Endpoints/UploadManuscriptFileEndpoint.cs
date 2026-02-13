using Articles.Abstractions;
using Articles.Abstractions.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Submission.API.Endpoints;

public static class UploadManuscriptFileEndpoint
{
    public static void Map(this IEndpointRouteBuilder app)
    {
        app.Map("api/articles/{articleId:int}/assets/manuscript:upload",
            async ([FromRoute]int articleId,[FromForm] UploadManuscriptFileCommand command, ISender sender) =>
            {
                var response = await sender.Send(command with { ArticleId = articleId });
                return Results.Created($"/api/articles/{command.ArticleId}/assets{response.Id}:download", response);
            })
            //.RequireRoleAuthorization(Role.CORAUT)
            .RequireAuthorization(policy => policy.RequireRole(Role.CORAUT))
            .WithName("UploadManuscript")
            .WithTags("Assets")
            .Produces<IdResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .DisableAntiforgery(); //because of IFormFile
    }
}
