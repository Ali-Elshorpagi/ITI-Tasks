using Lab04.Models;
using Lab04.Repositories;
using Lab04.Configurations;
using Lab04.Services;
using Lab04.Services.Contracts;
using Lab04.Services.Parsing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Lab04
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Logging.AddFilter("Microsoft.AspNetCore.Watch", LogLevel.None);

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ITIContext>(op =>
                op.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

            builder.Services.Configure<RagOptions>(builder.Configuration.GetSection("Rag"));
            builder.Services.Configure<OpenAiOptions>(builder.Configuration.GetSection("OpenAI"));

            builder.Services.AddIdentity<Student, IdentityRole>()
                .AddEntityFrameworkStores<ITIContext>();

            builder.Services.AddScoped<IAccounttRepository, AccountRepository>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IRagDocumentService, RagDocumentService>();
            builder.Services.AddScoped<IDocumentIngestionService, DocumentIngestionService>();
            builder.Services.AddScoped<IRagQueryService, RagQueryService>();
            builder.Services.AddScoped<ITextPreprocessor, TextPreprocessor>();
            builder.Services.AddScoped<IChunkingService, SlidingWindowChunkingService>();
            builder.Services.AddScoped<ITextExtractorResolver, TextExtractorResolver>();
            builder.Services.AddScoped<ITextExtractor, TxtTextExtractor>();
            builder.Services.AddScoped<ITextExtractor, PdfTextExtractor>();
            builder.Services.AddScoped<IVectorStore, VectorStore>();
            builder.Services.AddScoped<IRagReranker, SimpleRagReranker>();
            builder.Services.AddSingleton<IDocumentIngestionQueue, DocumentIngestionQueue>();
            builder.Services.AddHostedService<DocumentIngestionWorker>();
            builder.Services.AddHttpClient<IEmbeddingService, OpenAiEmbeddingService>((sp, client) =>
            {
                var options = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<OpenAiOptions>>().Value;
                client.BaseAddress = new Uri(options.BaseUrl);
            });
            builder.Services.AddHttpClient<ILlmService, OpenAiLlmService>((sp, client) =>
            {
                var options = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<OpenAiOptions>>().Value;
                client.BaseAddress = new Uri(options.BaseUrl);
            });

            builder.Services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(op =>
            {
                string key = builder.Configuration["Jwt:Key"] ?? string.Empty;
                var secKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                op.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = secKey,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
