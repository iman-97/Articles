using Blocks.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.ValueObjects;

namespace Submission.Persistence.EntityConfigurations;

internal class FileEntityConfiguration
{
    public void Configure(ComplexPropertyBuilder<File> builder)
    {
        builder.Property(x => x.OriginalName)
            .HasMaxLength(MaxLength.C256)
            .HasComment("Original full file name, with extension");

        builder.Property(x => x.FileServerId)
            .HasMaxLength(MaxLength.C64);

        builder.Property(x => x.Size)
            .HasComment("Size of the file in kilobytes");

        builder.ComplexProperty(x => x.Extension, b =>
        {
            b.Property(x => x.Value)
                .HasColumnName($"{b.Metadata.ClrType.Name}_{b.Metadata.PropertyInfo?.Name}")
                .HasMaxLength(MaxLength.C8);
        });

        builder.ComplexProperty(x => x.Name, b =>
        {
            b.Property(x => x.Value)
                .HasColumnName($"{b.Metadata.ClrType.Name}_{b.Metadata.PropertyInfo?.Name}")
                .HasMaxLength(MaxLength.C64);
        });
    }
}
