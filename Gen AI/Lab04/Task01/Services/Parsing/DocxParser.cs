using DocumentFormat.OpenXml.Packaging;

namespace Task01.Services.Parsing;

public sealed class DocxParser : IDocumentParser
{
    public bool CanParse(string extension) => extension == ".docx";

    public Task<string> ParseAsync(Stream stream, CancellationToken ct)
    {
        using var wordDoc = WordprocessingDocument.Open(stream, false);
        var body = wordDoc.MainDocumentPart?.Document.Body;
        var text = body?.InnerText ?? string.Empty;
        return Task.FromResult(text);
    }
}
