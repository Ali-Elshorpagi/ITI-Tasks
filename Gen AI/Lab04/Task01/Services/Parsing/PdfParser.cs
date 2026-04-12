using UglyToad.PdfPig;

namespace Task01.Services.Parsing;

public sealed class PdfParser : IDocumentParser
{
    public bool CanParse(string extension) => extension == ".pdf";

    public Task<string> ParseAsync(Stream stream, CancellationToken ct)
    {
        using var pdf = PdfDocument.Open(stream);
        var pages = pdf.GetPages().Select(p => p.Text);
        return Task.FromResult(string.Join(Environment.NewLine, pages));
    }
}
