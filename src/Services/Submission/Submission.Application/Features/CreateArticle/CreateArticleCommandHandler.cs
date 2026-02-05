using Articles.Abstractions;
using MediatR;
using Submission.Domain.Entities;
using Submission.Persistence.Repositories;
using Blocks.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Articles.Abstractions.Enums;

namespace Submission.Application.Features.CreateArticle;

internal class CreateArticleCommandHandler(Repository<Journal> journalRepository) : IRequestHandler<CreateArticleCommand, IdResponse>
{
    public async Task<IdResponse> Handle(CreateArticleCommand command, CancellationToken ct)
    {
        var journal = await journalRepository.FindByIdOrThrowAsync(command.JournalId);
        var article = journal.CreateArticle(command.Title, command.Type, command.Scope);

        await AssignCurrentUserAsAuthor(article, command);

        await journalRepository.SaveChangesAsync(ct);

        return new IdResponse(article.Id);
    }

    private async Task AssignCurrentUserAsAuthor(Article article, CreateArticleCommand command)
    {
        var author = await journalRepository.Context.Authors.SingleOrDefaultAsync(x => x.UserId == command.CreatedById);

        if (author is not null)
            article.AssignAuthor(author, [ContributionArea.OriginalDraft], true);
    }
}
