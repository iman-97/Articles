using FileStorage.Contracts;
using Submission.Domain.Entities;
using System.IO;

namespace Submission.Domain.ValueObjects;

public partial class File
{
    private File() { }

    internal static File CreateFile(UploadResponse uploadResponse,Asset asset, AssetTypeDefinition assetType)
    {
        var fileName = Path.GetFileName(uploadResponse.FilePath);
        var extention = FileExtension.FromFileName(fileName, assetType);

        var file = new File()
        {
            Name = FileName.FromAsset(asset, extention),
            Extension = extention,
            OriginalName = fileName,
            Size = uploadResponse.FileSize,
            FileServerId = uploadResponse.FileId
        };

        return file;
    }
}
