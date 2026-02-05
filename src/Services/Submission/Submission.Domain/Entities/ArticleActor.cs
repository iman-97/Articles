using Articles.Abstractions.Enums;

namespace Submission.Domain.Entities;

public class ArticleActor
{
    public int ArticleId { get; init; }
    public int PersonId { get; init; }

    public UserRoleType Role {  get; init; }

    public Article Article { get; init; } = null!;
    public Person Person { get; init; } = null!;

    public string TypeDiscriminator { get; init; } = null!; //EF Discriminator
}
