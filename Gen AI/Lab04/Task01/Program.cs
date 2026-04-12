using Task01.Data.Repositories;
using Task01.Models;
using Task01.Services.Chunking;
using Task01.Services.Embedding;
using Task01.Services.Generation;
using Task01.Services.Ingestion;
using Task01.Services.Parsing;
using Task01.Services.Retrieval;

namespace Task01;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        builder.Services.Configure<RagOptions>(builder.Configuration.GetSection("Rag"));
        builder.Services.Configure<OpenAiOptions>(builder.Configuration.GetSection("OpenAI"));
        builder.Services.Configure<VectorStoreOptions>(builder.Configuration.GetSection("VectorStore"));
        builder.Services.Configure<MongoDbOptions>(builder.Configuration.GetSection("MongoDb"));
        builder.Services.AddHttpClient();

        builder.Services.AddSingleton<IDocumentRepository, InMemoryDocumentRepository>();
        builder.Services.AddSingleton<IChunkRepository, InMemoryChunkRepository>();

        var vectorProvider = builder.Configuration["VectorStore:Provider"];
        if (string.Equals(vectorProvider, "Mongo", StringComparison.OrdinalIgnoreCase) || string.Equals(vectorProvider, "MongoDB", StringComparison.OrdinalIgnoreCase))
        {
            builder.Services.AddSingleton<IVectorRepository, MongoVectorRepository>();
        }
        else if (string.Equals(vectorProvider, "Chroma", StringComparison.OrdinalIgnoreCase))
        {
            builder.Services.AddSingleton<IVectorRepository, ChromaVectorRepository>();
        }
        else if (string.Equals(vectorProvider, "Qdrant", StringComparison.OrdinalIgnoreCase))
        {
            builder.Services.AddSingleton<IVectorRepository, QdrantVectorRepository>();
        }
        else
        {
            builder.Services.AddSingleton<IVectorRepository, InMemoryVectorRepository>();
        }

        builder.Services.AddSingleton<IDocumentParser, TextParser>();
        builder.Services.AddSingleton<IDocumentParser, PdfParser>();
        builder.Services.AddSingleton<IDocumentParser, DocxParser>();

        builder.Services.AddSingleton<IChunker, RecursiveChunker>();
        builder.Services.AddSingleton<IEmbeddingService, HashingEmbeddingService>();
        builder.Services.AddSingleton<IRetrievalService, RetrievalService>();
        builder.Services.AddSingleton<IAnswerGenerator, AnswerGenerator>();
        builder.Services.AddSingleton<IIngestionQueue, InMemoryIngestionQueue>();
        builder.Services.AddSingleton<IIngestionService, IngestionService>();
        builder.Services.AddHostedService<IngestionBackgroundService>();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}
