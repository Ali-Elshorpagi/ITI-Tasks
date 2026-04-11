using System.Text.RegularExpressions;
using Lab04.Services.Contracts;

namespace Lab04.Services
{
    public class TextPreprocessor : ITextPreprocessor
    {
        public string Normalize(string rawText)
        {
            if (string.IsNullOrWhiteSpace(rawText))
            {
                return string.Empty;
            }

            var withoutControlChars = Regex.Replace(rawText, @"\p{C}+", " ");
            return Regex.Replace(withoutControlChars, @"\s+", " ").Trim();
        }
    }
}
