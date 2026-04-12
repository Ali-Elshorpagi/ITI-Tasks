namespace Task01.Services.Parsing;

public interface IDocumentParser
{
    bool CanParse(string extension);
    Task<string> ParseAsync(Stream stream, CancellationToken ct);
}
