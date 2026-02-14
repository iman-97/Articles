using Blocks.EntityFramework;
using FileStorage.Contracts;

namespace Submission.Application.Features.UploadFile.UploadManuscriptFile;

public class UploadManuscriptFileCommandHandler(ArticleRepository articleRepository,
    AssetTypeDefinitionRepository assetTypeRepo, IFileService fileService)
    : IRequestHandler<UploadManuscriptFileCommand, IdResponse>
{
    public async Task<IdResponse> Handle(UploadManuscriptFileCommand command, CancellationToken cancellationToken)
    {
        var article = await articleRepository.GetByIdOrThrowAsync(command.ArticleId);
        var assetType = assetTypeRepo.GetById(command.AssetType);

        Asset asset = null;

        if (assetType.AllowsMultipleAssets == false)
            asset = article.Assets.SingleOrDefault(x => x.Type == assetType.Id);

        if (asset is null)
            asset = article.CreateAsset(assetType);

        var filePath = asset.GenerateStorageFilePath(command.File.FileName);
        var uploadRes = await fileService.UploadFileAsync(
            filePath,
            command.File,
            overwrite: true,
            tags: new Dictionary<string, string>
            {
                {"entity", nameof(Asset) },
                {"entityId",asset.Id.ToString() }
            });

        try
        {
            asset.CreateFile(uploadRes, assetType);

            await articleRepository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            await fileService.TryDeleteFileAsync(uploadRes.FileId);
            throw;
        }

        return new IdResponse(asset.Id);
    }
}
