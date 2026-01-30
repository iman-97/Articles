using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blocks.EntityFramework;

public static class BuilderExtensions
{
    public static PropertyBuilder<TEnum> HasEnumConversion<TEnum>(this PropertyBuilder<TEnum> builder) where TEnum : Enum
    {
        return builder
            .HasConversion(
                x => x.ToString(),
                x => (TEnum)Enum.Parse(typeof(TEnum), x));
    }
}
