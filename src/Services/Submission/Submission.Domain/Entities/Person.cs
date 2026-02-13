using Blocks.Domain.Entities;
using Submission.Domain.ValueObjects;
using System.Collections.Generic;

namespace Submission.Domain.Entities;

public class Person : Entity
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }

    public string FullName => FirstName + " " + LastName;

    public string? Title { get; init; }
    public required EmailAddress EmailAddress { get; init; }
    public required string Affiliation { get; init; }

    public int? UserId { get; init; }

    //becuase article can only change article actor in this class its readonly
    public IReadOnlyList<ArticleActor> Actors { get; private set; } = new List<ArticleActor>();

    public string TypeDiscriminator { get; init; } = null!; //EF Discriminator
}
