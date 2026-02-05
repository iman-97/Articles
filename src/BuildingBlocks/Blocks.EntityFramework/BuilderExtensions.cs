using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

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

    public static PropertyBuilder<T> HasJsonCollectionConvertion<T>(this PropertyBuilder<T> builder)
        => builder.HasConversion(BuildJsonListConvertor<T>());

    public static ValueConverter<TCollection, string> BuildJsonListConvertor<TCollection>()
    {
        Func<TCollection, string> serializeFunc = v => JsonSerializer.Serialize(v);
        Func<string, TCollection> deserializeFunc = v => JsonSerializer.Deserialize<TCollection>(v ?? "[]");

        return new ValueConverter<TCollection, string>(
            x => serializeFunc(x),
            x => deserializeFunc(x));
    }
}
