using Blocks.Core;
using Blocks.EntityFramework;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

internal class AssetEntityConfiguration : EntityConfigurations<Asset>
{
    public override void Configure(EntityTypeBuilder<Asset> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Type)
            .HasEnumConversion();

        builder.ComplexProperty(x => x.Name, b =>
        {
            b.Property(c => c.Value)
                .HasColumnName(b.Metadata.PropertyInfo?.Name)
                .HasMaxLength(MaxLength.C64)
                .IsRequired();
        });

        builder.ComplexProperty(x => x.File, b =>
        {
            new FileEntityConfiguration().Configure(b);
        });
    }
}
