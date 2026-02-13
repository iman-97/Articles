using Blocks.Core;
using Blocks.Domain.ValueObjects;
using Submission.Domain.Entities;
using System.IO;

namespace Submission.Domain.ValueObjects;

public class FileExtension : StringValueObject
{
    private FileExtension(string value) => Value = value;

    public static FileExtension FromFileName(string fileName, AssetTypeDefinition assetType)
    {
        var extension = Path.GetExtension(fileName).Remove(0, 1); //removing .
        Guard.ThrowIfNullOrWhiteSpace(extension);
        Guard.ThrowIfNotEqual(
            assetType.AllowedFileExtensions.IsValidExtionsion(extension), true);

        return new FileExtension(extension);
    }
}
