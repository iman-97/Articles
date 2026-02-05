namespace Blocks.Core;

public static class StringExtentions
{
    public static string FormatWith(this string str, params object[] args)
        => string.Format(str, args);

    public static string FormatWith(this string str, object args)
        => string.Format(str, args);
}
