using Lab04.Services.Contracts;

namespace Lab04.Services.Parsing
{
    public class TextExtractorResolver : ITextExtractorResolver
    {
        private readonly IEnumerable<ITextExtractor> _extractors;

        public TextExtractorResolver(IEnumerable<ITextExtractor> extractors)
        {
            _extractors = extractors;
        }

        public ITextExtractor Resolve(string extension)
        {
            var extractor = _extractors.FirstOrDefault(e => e.CanExtract(extension));
            if (extractor is null)
            {
                throw new InvalidOperationException($"Unsupported extension: {extension}");
            }

            return extractor;
        }
    }
}
