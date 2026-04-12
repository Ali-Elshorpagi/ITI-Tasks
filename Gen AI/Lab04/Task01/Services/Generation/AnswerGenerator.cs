using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Task01.Models;
using Task01.Services.Retrieval;

namespace Task01.Services.Generation;

public sealed class AnswerGenerator : IAnswerGenerator
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly OpenAiOptions _openAiOptions;
    private readonly ILogger<AnswerGenerator> _logger;

    public AnswerGenerator(
        IHttpClientFactory httpClientFactory,
        IOptions<OpenAiOptions> openAiOptions,
        ILogger<AnswerGenerator> logger)
    {
        _httpClientFactory = httpClientFactory;
        _openAiOptions = openAiOptions.Value;
        _logger = logger;
    }

    public async Task<AskResponse> GenerateAsync(string question, IReadOnlyList<RetrievedChunk> chunks, CancellationToken ct)
    {
        var response = new AskResponse
        {
            Answer = chunks.Count == 0
                ? "I do not have enough context in the uploaded documents to answer that."
                : await GenerateAnswerAsync(question, chunks, ct),
            Sources = chunks.Select(c => new SourceCitation
            {
                DocumentId = c.DocumentId,
                FileName = c.FileName,
                ChunkIndex = c.ChunkIndex,
                Score = c.Score
            }).ToList()
        };

        return response;
    }

    private async Task<string> GenerateAnswerAsync(string question, IReadOnlyList<RetrievedChunk> chunks, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(_openAiOptions.ApiKey))
        {
            return BuildFallbackAnswer(question, chunks);
        }

        try
        {
            return await GenerateOpenAiAnswerAsync(question, chunks, ct);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Falling back to grounded text answer because the OpenAI chat call failed.");
            return BuildFallbackAnswer(question, chunks);
        }
    }

    private async Task<string> GenerateOpenAiAnswerAsync(string question, IReadOnlyList<RetrievedChunk> chunks, CancellationToken ct)
    {
        var context = string.Join("\n\n", chunks.Take(6).Select(c => $"[Source: {c.FileName}, Chunk {c.ChunkIndex}]\n{c.Content}"));
        var client = CreateClient();

        var builder = Kernel.CreateBuilder();
        builder.AddOpenAIChatCompletion(_openAiOptions.ChatModel, _openAiOptions.ApiKey, httpClient: client);
        var kernel = builder.Build();

        var prompt = @"You are a precise RAG assistant. Answer using only the provided context. Be concise, structured, and helpful. If the context is insufficient, say so clearly.

Context:
{{$context}}

Question:
{{$question}}

Write a clean answer with a short summary, key points, and a direct conclusion. Use simple markdown bullets when helpful.";

        var result = await kernel.InvokePromptAsync(prompt, new KernelArguments
        {
            ["context"] = context,
            ["question"] = question
        }, cancellationToken: ct);

        var content = result.ToString();
        return string.IsNullOrWhiteSpace(content)
            ? BuildFallbackAnswer(question, chunks)
            : content.Trim();
    }

    private HttpClient CreateClient()
    {
        var client = _httpClientFactory.CreateClient(nameof(AnswerGenerator));
        client.BaseAddress = new Uri(_openAiOptions.BaseUrl.TrimEnd('/') + "/");
        return client;
    }

    private static string BuildFallbackAnswer(string question, IReadOnlyList<RetrievedChunk> chunks)
    {
        var topChunks = chunks.Take(4).ToList();
        var keyPoints = topChunks
            .SelectMany(c => SplitIntoSentences(c.Content))
            .Select(NormalizeText)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Take(3)
            .ToList();

        var summary = BuildSummary(question, topChunks);
        var conclusion = BuildConclusion(question, topChunks);

        var builder = new StringBuilder();
        builder.AppendLine("Summary");
        builder.AppendLine(summary);
        builder.AppendLine();
        builder.AppendLine("Key points");

        if (keyPoints.Count > 0)
        {
            foreach (var point in keyPoints)
            {
                builder.AppendLine($"- {TrimToLength(point, 180)}");
            }
        }
        else
        {
            foreach (var chunk in topChunks)
            {
                builder.AppendLine($"- {TrimToLength(NormalizeText(chunk.Content), 180)}");
            }
        }

        builder.AppendLine();
        builder.AppendLine("Answer");
        builder.AppendLine(conclusion);
        return builder.ToString().Trim();
    }

    private static string BuildSummary(string question, IReadOnlyList<RetrievedChunk> chunks)
    {
        if (chunks.Count == 0)
        {
            return $"I could not find enough grounded context to answer: {question}.";
        }

        var firstSentence = SplitIntoSentences(chunks[0].Content)
            .Select(NormalizeText)
            .FirstOrDefault(s => !string.IsNullOrWhiteSpace(s));

        if (!string.IsNullOrWhiteSpace(firstSentence))
        {
            return $"The uploaded document appears to be about {TrimToLength(firstSentence, 220)}";
        }

        return $"The uploaded document appears to contain relevant context for: {question}.";
    }

    private static IEnumerable<string> SplitIntoSentences(string text)
    {
        return text.Split(new[] { ". ", "? ", "! ", "\n" }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }

    private static string BuildConclusion(string question, IReadOnlyList<RetrievedChunk> chunks)
    {
        if (chunks.Count == 0)
        {
            return $"I could not find enough grounded context to answer: {question}.";
        }

        var supportingText = chunks
            .SelectMany(c => SplitIntoSentences(c.Content))
            .Select(NormalizeText)
            .FirstOrDefault(s => !string.IsNullOrWhiteSpace(s));

        if (string.IsNullOrWhiteSpace(supportingText))
        {
            return $"I can answer this only at a high level right now: {question}.";
        }

        return $"Based on the retrieved context, the safest grounded answer is: {TrimToLength(supportingText, 260)}";
    }

    private static string NormalizeText(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return string.Empty;
        }

        return string.Join(" ", text.Split(default(char[]), StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
    }

    private static string TrimToLength(string text, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(text) || text.Length <= maxLength)
        {
            return text;
        }

        return text[..(maxLength - 3)].TrimEnd() + "...";
    }
}



