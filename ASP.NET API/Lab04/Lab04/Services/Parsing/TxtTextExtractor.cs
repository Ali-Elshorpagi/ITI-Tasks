using Lab04.Services.Contracts;

namespace Lab04.Services.Parsing
{
    public class TxtTextExtractor : ITextExtractor
    {
        public bool CanExtract(string extension) => extension.Equals(".txt", StringComparison.OrdinalIgnoreCase) || extension.Equals(".md", StringComparison.OrdinalIgnoreCase);

        public async Task<string> ExtractAsync(string filePath, CancellationToken cancellationToken)
        {
            using var reader = File.OpenText(filePath);
            return await reader.ReadToEndAsync();
        }
    }
}
