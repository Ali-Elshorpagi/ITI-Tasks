using System.Text.RegularExpressions;

namespace Task01.Services;

public static class TextPreprocessor
{
    public static string Normalize(string text)
    {
        var normalized = text.Replace("\r\n", "\n");
        normalized = Regex.Replace(normalized, "\\s+", " ").Trim();
        return normalized;
    }
}
