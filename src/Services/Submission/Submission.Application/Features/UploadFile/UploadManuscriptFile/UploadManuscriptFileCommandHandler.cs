using Blocks.EntityFramework;

namespace Submission.Application.Features.UploadFile.UploadManuscriptFile;

public class UploadManuscriptFileCommandHandler(ArticleRepository articleRepository,
    AssetTypeDefinitionRepository assetTypeRepo)
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

        await articleRepository.SaveChangesAsync(cancellationToken);

        return new IdResponse(asset.Id);
    }
}
