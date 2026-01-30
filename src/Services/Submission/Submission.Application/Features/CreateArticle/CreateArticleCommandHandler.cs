using Articles.Abstractions;
using MediatR;
using Submission.Domain.Entities;
using Submission.Persistence.Repositories;
using Blocks.EntityFramework;

namespace Submission.Application.Features.CreateArticle;

internal class CreateArticleCommandHandler(Repository<Journal> journalRepository) : IRequestHandler<CreateArticleCommand, IdResponse>
{
    public async Task<IdResponse> Handle(CreateArticleCommand command, CancellationToken ct)
    {
        var journal = await journalRepository.FindByIdOrThrowAsync(command.JournalId);
        var article = journal.CreateArticle(command.Title, command.Type, command.Scope);

        await journalRepository.SaveChangesAsync(ct);

        return new IdResponse(article.Id);
    }
}
