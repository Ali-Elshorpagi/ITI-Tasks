namespace Lab04.Services.Contracts
{
    public interface ITextPreprocessor
    {
        string Normalize(string rawText);
    }
}
