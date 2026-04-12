namespace Task01.Services.Parsing;

public sealed class TextParser : IDocumentParser
{
    public bool CanParse(string extension) => extension is ".txt" or ".md" or ".csv";

    public async Task<string> ParseAsync(Stream stream, CancellationToken ct)
    {
        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync(ct);
    }
}
