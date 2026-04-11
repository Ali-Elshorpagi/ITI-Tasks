# Lab04 - Identity, JWT & RAG AI Integration

## Description

This lab is an advanced enterprise-level realization showcasing how modern **ASP.NET Core Web API** can handle complex Auth scenarios and integrate seamlessly with cutting-edge **Generative AI** flows. Specifically, it establishes an end-to-end framework for **Authentication**, securely routing calls, and constructing an extensive **Retrieval-Augmented Generation (RAG)** pipeline.

### Key Concepts & Implementations

- **ASP.NET Core Identity & JWT Authentication**: 
  - Extending default tables with `IdentityRole` inside `ITIContext`.
  - Crafting an `AccountController` equipped with `Register` and `Login` functionalities via DTO mapping.
  - Generating standard encrypted Bearer JSON Web Tokens (JWT) acting as an exchange passport for authorizing HTTP calls across subsequent requests securely across endpoints.
- **Enterprise-ready RAG Pipeline Infrastructure**: 
  - Designing a robust multi-pass process combining `IDocumentIngestionService` supporting parsing complex `.txt` and proprietary structured files like `.pdf`.
  - Implementing dedicated `TextExtractors` and specialized formatting `TextPreprocessors`.
  - Managing large tokens efficiently via strategies like `SlidingWindowChunkingService` parsing raw massive context logs.
- **OpenAI Interfacing & Vector Workloads**: 
  - Structuring highly available connections to Large Language Models (`ILlmService` -> `OpenAiLlmService`) via injected typed HTTP configurations bound directly from `appsettings.json` via the Options Pattern (`IOptions<OpenAiOptions>`).
  - Leveraging specific `EmbeddingService` models to map documents to embedding vectors which are subsequently pushed into a persistent `VectorStore` structure allowing semantic search querying inside the `RagQueryController`.
- **Background Multi-threading Tasks Workers**: 
  - Ensuring the application scales efficiently without blocking web UI requests. Orchestrating extremely CPU/Network-heavy operations (e.g., PDF parsing, hitting external AI endpoints, writing vector chunks) utilizing concurrent worker pipelines. 
  - Managing AI queues via the `DocumentIngestionQueue` working hand-in-hand with persistent .NET `IHostedService` frameworks (`DocumentIngestionWorker`) constantly probing background duties invisibly.

### Directory Structure
- **Lab04**: A highly orchestrated codebase heavily using DI (Dependency Injection), separated into `Controllers` (`Account`, `RagDocuments`, `RagQuery`), Data Configurations/Migrations, Options Pattern implementations, comprehensive RAG pipeline services, and Abstract Repositories logic making this an excellent benchmark for heavy monolithic setups mimicking microservice AI routines.