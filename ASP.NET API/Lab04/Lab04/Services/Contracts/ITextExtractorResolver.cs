namespace Lab04.Services.Contracts
{
    public interface ITextExtractorResolver
    {
        ITextExtractor Resolve(string extension);
    }
}
