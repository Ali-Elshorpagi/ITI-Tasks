# Task01 - RAG Pipeline Demo (.NET 10)

This project is a complete end-to-end Demonstration of a Retrieval-Augmented Generation (RAG) pipeline built with ASP.NET Core (.NET 10) and Razor Pages.

It uses:
- **MongoDB Atlas** as the primary vector store.
- **MongoDB** for vector persistence and storage.
- **Microsoft Semantic Kernel** to orchestrate OpenAI calls for embeddings (`text-embedding-3-small`) and answers (`gpt-4o-mini`).
- A polished **Claude-style chat UI** with a left-side panel displaying chunk vectors for the uploaded document.
- **Resilient fallback architectures** to keep the app working when external services (MongoDB, OpenAI API) are offline.

## How to run and use this project

### Prerequisites
1. **.NET 10 SDK** installed on your machine.
2. A free **MongoDB Atlas** cluster (or local MongoDB connection string).
3. An **OpenAI API Key** (optional, but recommended for high-quality responses).

### Setup
1. Clone the repository and navigate to the `Task01` directory.
2. Open `appsettings.json` (or use .NET Secret Manager) and configure your secrets:
   - Set `OpenAI:ApiKey` to your OpenAI API key.
   - Set `MongoDb:ConnectionString` to your MongoDB connection string.
3. Open a terminal and run the application:
   ```powershell
   dotnet restore
   dotnet run
   ```
4. Open your browser and navigate to the URL shown in the console (usually `https://localhost:7144`).

### Using the UI
1. **Upload Document:** On the left panel, click "Browse files", select a supported document (PDF, TXT, MD, DOCX, CSV), and click **Upload and Process**.
2. **Observe Indexing:** The status badge will change from *Queued* to *Processing* to *Completed*. You will see the chunk vectors appear in the "MongoDB vector data" panel with their dimensionality and previews.
3. **Chat:** Once indexing completes, go to the right panel. Type a question (or click a prompt bubble) and press `Enter`.
4. **View Results:** The assistant will reply with a detailed answer, citing the source chunks beneath the message. A **Relevance Score** badge will appear next to your question, showing how closely the retrieved context matched your prompt.

---

## Architecture and technical details

### Microsoft Semantic Kernel integration
Instead of calling the OpenAI REST API directly, this project uses **Microsoft.SemanticKernel** to manage integrations and prompt templates.

- **Embeddings Implementation:** In `HashingEmbeddingService.cs`, we use the `ITextEmbeddingGenerationService` mapped to OpenAI boundaries.
  ```csharp
  var builder = Kernel.CreateBuilder();
  builder.AddOpenAITextEmbeddingGeneration(_openAiOptions.EmbeddingModel, _openAiOptions.ApiKey, httpClient: client);
  var kernel = builder.Build();
  var embeddingService = kernel.GetRequiredService<ITextEmbeddingGenerationService>();
  var embeddings = await embeddingService.GenerateEmbeddingsAsync(texts.ToList(), cancellationToken: ct);
  ```

- **Answer Generation Implementation:** In `AnswerGenerator.cs`, the framework executes prompting with declarative templates, combining retrieved structural context arrays into dynamic `{{$context}}` variables evaluated dynamically alongside user questions `{{$question}}`.
  ```csharp
  var prompt = @"You are a precise RAG assistant. Answer using only the provided context...
  Context:
  {{$context}}
  Question:
  {{$question}}
  Write a clean answer with a short summary, key points, and a direct conclusion.";
  var result = await kernel.InvokePromptAsync(prompt, new KernelArguments { ["context"] = context, ["question"] = question }, cancellationToken: ct);
  ```

- **SK Warnings:** Since certain generic Semantic Kernel embedding services are considered experimental, `SKEXP0001` and `SKEXP0010` warnings are bypassed in `Task01.csproj`. 

### Chunking strategy
To feed accurate context to the AI, documents must be split into smaller, manageable chunks.

- **Configured Strategy:** `FixedSizeOverlapping`
- **Chunk Size:** `1000` characters. This controls the maximum size of a chunk.
- **Overlap:** `180` characters. This ensures that the end of one chunk is repeated at the beginning of the next. **Why?** It prevents critical context (like a sentence spanning across a chunk boundary) from being lost.
- **Recursive Splitting:** The logic naturally attempts to break text at clean boundaries (double newlines, single newlines, punctuation, then spaces) rather than cutting words in half (`Services/Chunking/RecursiveChunker.cs`).

### Vector provider strategies
A vector database searches for similarities between your question and the document chunks based on multi-dimensional numeric representations (vectors).

- **Primary Provider (MongoDB):** Highly integrated storage. `VectorRecord` entities store the `ChunkId` and `DocumentId` as plain strings directly (`[BsonRepresentation(BsonType.String)]`), making them match the UI exactly and easily readable in MongoDB Compass.
- **Caching for Speed:** To reduce chat latency, the `MongoVectorRepository` caches vectors in memory after the first load.
- **Failover / Fallback:** If MongoDB goes offline or the connection string is invalid, the app catches the exception and immediately falls back to an `InMemoryVectorRepository`. The UI will still work for the current session.
- **Alternative Providers:** The project code includes full implementations for **ChromaDB** and **Qdrant**. You can switch to them by changing `VectorStore:Provider` to `"Chroma"` or `"Qdrant"` in `appsettings.json` and running those services locally via Docker.

### Fallback models
- **Embeddings:** If the OpenAI API key is missing or the call fails, it routes to `HashingEmbeddingService.cs`, which generates deterministic pseudo-embeddings locally using SHA256 hashes.
- **Answer Generation:** If OpenAI is offline, `AnswerGenerator.cs` extracts key sentences from the highest-matching chunks and formats a local, structured response with a Summary, Key Points, and Conclusion.

---

## Configuration (`appsettings.json`)

```json
{
  "Rag": {
    "UploadFolder": "App_Data/uploads",
    "ChunkStrategy": "FixedSizeOverlapping",
    "ChunkSize": 1000,
    "ChunkOverlap": 180,
    "EmbeddingDimensions": 256,
    "MinScore": 0.1,
    "MaxRetrievedChunks": 8
  },
  "OpenAI": {
    "ApiKey": "YOUR_OPENAI_API_KEY",
    "BaseUrl": "https://api.openai.com/v1",
    "EmbeddingModel": "text-embedding-3-small",
    "ChatModel": "gpt-4o-mini"
  },
  "MongoDb": {
    "ConnectionString": "mongodb+srv://...",
    "Database": "Task01",
    "VectorCollection": "rag_vectors"
  },
  "VectorStore": {
    "Provider": "Mongo"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.AspNetCore.Hosting.Diagnostics": "Warning"
    }
  }
}
```
*Note: Console logging from ASP.NET Core is minimized. The terminal only prints critical system routes and customized API lifecycle logs (e.g., `/chat` or `/documents` execution times).*

---

## Internal API flow

1. `POST /documents/upload`
2. Background ingestion triggers -> file is parsed, pre-processed, chunked via the `RecursiveChunker`.
3. Semantic Kernel requests embeddings from OpenAI (`HashingEmbeddingService`).
4. Embeddings metadata stored directly in MongoDB via `MongoVectorRepository`.
5. Frontend polls `/documents/{id}` until the `Completed` status arrives.
6. The `MongoDB vector data` panel fills via a call to `/documents/{id}/vectors`.
7. User submits questions to `POST /chat/ask`.
8. Semantic Kernel creates an embedding for the question, then performs semantic/lexical similarity retrieval (`RetrievalService`).
9. Matched chunks flow to OpenAI's GPT via a Semantic Kernel pipeline prompt (`InvokePromptAsync`) for summarization alongside a custom relevance calculation (`ChatController.cs`).
10. UI updates rendering of Chat answers alongside a calculated percentage-relevance badge.
