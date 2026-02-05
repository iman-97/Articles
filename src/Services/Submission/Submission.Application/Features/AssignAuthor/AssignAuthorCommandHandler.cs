using Blocks.EntityFramework;

namespace Submission.Application.Features.AssignAuthor;

public class AssignAuthorCommandHandler(ArticleRepository articleRepository)
    : IRequestHandler<AssignAuthorCommand, IdResponse>
{
    public async Task<IdResponse> Handle(AssignAuthorCommand command, CancellationToken cancellationToken)
    {
        var article = await articleRepository.GetByIdOrThrowAsync(command.ArticleId);

        var author = await articleRepository.Context.Authors.FindByIdOrThrowAsync(command.AuthorId);

        article.AssignAuthor(author, command.ContributionAreas, command.IsCorrespondingAuthor);

        await articleRepository.SaveChangesAsync(cancellationToken);

        return new IdResponse(article.Id);
    }
}
