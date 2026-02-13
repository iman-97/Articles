using Blocks.Core.FluentValidations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Submission.Application.Features.UploadFile;

public record UploadManuscriptFileCommand : ArticleCommand
{
    /// <summary>
    /// the asset type of file
    /// </summary>
    [Required]
    public AssetType AssetType { get; init; }

    /// <summary>
    /// the file to be uploaded
    /// </summary>
    [Required]
    public IFormFile File { get; init; } = null!;

    public override ArticleActionType ActionType => ArticleActionType.Upload;
}

public class UploadManuscriptFileCommandValidator : ArticleCommandValidator<UploadManuscriptFileCommand>
{
    public UploadManuscriptFileCommandValidator()
    {
        RuleFor(x => x.File)
            .NotNullWithMessage();

        RuleFor(x => x.AssetType)
            .Must(IsAssetTypeAllowed)
            .WithMessage(x => $"{x.AssetType} not allowed");
    }

    public IReadOnlyCollection<AssetType> AllowedAssetTypes => new HashSet<AssetType> { AssetType.Manuscript };

    private bool IsAssetTypeAllowed(AssetType assetType) => AllowedAssetTypes.Contains(assetType);
}