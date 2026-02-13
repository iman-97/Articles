using Articles.Abstractions.Enums;
using Blocks.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Submission.Domain.Entities;

public partial class Article
{
    public void AssignAuthor(Author author, HashSet<ContributionArea> contributionAreas, bool isCorrespondingAuthor)
    {
        var role = isCorrespondingAuthor ? UserRoleType.CORAUT : UserRoleType.AUT;

        if (Actors.Exists(x => x.PersonId == author.Id && x.Role == role))
            throw new DomainException($"Author {author.EmailAddress} is already assigned to the article");

        Actors.Add(new ArticleAuthor
        {
            ContributionAreas = contributionAreas,
            Person = author,
            Role = role,
        });
    }

    public Asset CreateAsset(AssetTypeDefinition assetType)
    {
        var assetCount = _assets
            .Where(x => x.Type == assetType.Id)
            .Count();

        if (assetType.MaxAssetCount > assetCount - 1) //its wrong i guess
            throw new DomainException($"The maximum number of files allowe for {assetType.Name} was already reached");

        var asset = Asset.Create(this, assetType);
        _assets.Add(asset);

        return asset;
    }
}
