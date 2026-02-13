using Blocks.Domain.ValueObjects;
using Submission.Domain.Entities;

namespace Submission.Domain.ValueObjects;

public class AssetName : StringValueObject
{
    private AssetName(string value) => Value = value;

    public static AssetName FromAssetType(AssetTypeDefinition assetType)
    {
        return new AssetName(assetType.Name.ToString());
    }
}
