using Submission.Domain.ValueObjects;

namespace Submission.Domain.Entities;

public partial class Asset
{
    private Asset() { }

    internal static Asset Create(Article article, AssetTypeDefinition assetType)
    {
        return new Asset
        {
            ArticleId = article.Id,
            Article = article,
            Name = AssetName.FromAssetType(assetType),
            Type = assetType.Name
        };
    }
}
