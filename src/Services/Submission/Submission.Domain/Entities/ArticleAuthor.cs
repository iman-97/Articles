using Articles.Abstractions.Enums;
using System.Collections.Generic;

namespace Submission.Domain.Entities;

public class ArticleAuthor : ArticleActor
{
    public HashSet<ContributionArea> ContributionAreas { get; init; } = null!;
}
