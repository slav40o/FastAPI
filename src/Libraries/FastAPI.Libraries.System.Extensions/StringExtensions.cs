using System.Text.RegularExpressions;

namespace System;

/// <summary>
/// Defines extension methods on the <see cref="string"/> class.
/// </summary>
public static partial class StringExtensions
{
    [RegexGenerator("</?\\w+((\\s+\\w+(\\s*=\\s*(?:\".*?\"|'.*?'|[^'\">\\s]+))?)+\\s*|\\s*)/?>", RegexOptions.Compiled)]
    private static partial Regex HtmlTagRegex();
    private static readonly Regex htmlTagsRegex = HtmlTagRegex();

    /// <summary>
    /// Determines whether the instance is composed entirely of whitespace characters.
    /// </summary>
    /// <param name="value">String instance.</param>
    /// <returns>True if string is whitespace, otherwise false.</returns>
    public static bool IsWhiteSpace(this string value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    /// Converts the first letter of a given string value to upped case.
    /// </summary>
    /// <param name="value">String value.</param>
    /// <returns>Capitalized string value.</returns>
    public static string Capitalize(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return value;
        }

        if (value.Length == 1)
        {
            return value.ToUpperInvariant();
        }

        var span = value.AsSpan();
        return $"{char.ToUpper(span[0])}{span[1..]}";
    }

    /// <summary>
    /// Remove all HTML tags from given string and leave only their content.
    /// </summary>
    /// <param name="html">HTML string</param>
    /// <returns>Stripped HTML</returns>
    public static string StripHtmlTags(this string html)
    {
        return htmlTagsRegex.Replace(html, string.Empty);
    }

    /// <summary>
    /// Split string using capital letters as delimiter.
    /// </summary>
    /// <param name="value">String value.</param>
    /// <returns>Separates string values.</returns>
    public static IList<string> SplitByUpperCase(this string value)
    {
        var parts = new List<string>();
        if (string.IsNullOrWhiteSpace(value))
        {
            return parts;
        }

        if (value.Length == 1)
        {
            parts.Add(value);
            return parts;
        }

        var span = value.AsSpan();
        int startIndex = 0;
        while (startIndex < span.Length)
        {
            int length = GetNextSplicePart(span, startIndex);
            parts.Add(new string(span.Slice(startIndex, length)).Trim());
            startIndex += length;
        }

        return parts;
    }

    private static int GetNextSplicePart(ReadOnlySpan<char> span, int startIndex)
    {
        int index = startIndex + 1;
        while (index < span.Length)
        {
            if (index >= span.Length || char.IsUpper(span[index]))
            {
                return index - startIndex;
            }

            index++;
        }

        return index - startIndex;
    }
}
