using Lab04.Services.Contracts;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab04.Services.Parsing
{
    public class PdfTextExtractor : ITextExtractor
    {
        public bool CanExtract(string extension) => extension.Equals(".pdf", StringComparison.OrdinalIgnoreCase);

        public async Task<string> ExtractAsync(string filePath, CancellationToken cancellationToken)
        {
            // Lightweight text extraction fallback without external PDF parser dependency.
            var bytes = await File.ReadAllBytesAsync(filePath, cancellationToken);
            var text = Encoding.Latin1.GetString(bytes);

            var matches = Regex.Matches(text, @"\(([^\)]{2,})\)");
            var extracted = new StringBuilder();
            foreach (Match match in matches)
            {
                if (match.Groups.Count > 1)
                {
                    extracted.Append(' ').Append(match.Groups[1].Value);
                }
            }

            var result = extracted.ToString().Trim();
            if (string.IsNullOrWhiteSpace(result))
            {
                throw new InvalidOperationException("Unable to extract text from PDF with built-in parser. Please use text-based PDFs.");
            }

            return result;
        }
    }
}
