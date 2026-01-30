using Blocks.EntityFramework;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

internal class ArticleEntityConfiguration : EntityConfigurations<Article>
{
    public override void Configure(EntityTypeBuilder<Article> builder)
    {
        base.Configure(builder);

        builder
            .Property(x => x.Title)
            .HasMaxLength(256)
            .IsRequired();

        builder
            .Property(x => x.Scope)
            .HasMaxLength(2048)
            .IsRequired();

        builder
            .Property(x => x.Stage)
            .HasEnumConversion();

        builder
            .Property(x => x.Type)
            .HasEnumConversion();

        builder
            .HasOne(x => x.Journal)
            .WithMany(x => x.Articles)
            .HasForeignKey(x => x.journalId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
