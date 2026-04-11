using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lab04.Models
{
    public class ITIContext : IdentityDbContext<Student>
    {
        public ITIContext(DbContextOptions<ITIContext> option) : base(option) { }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<RagDocument> RagDocuments { get; set; }
        public virtual DbSet<RagChunk> RagChunks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RagDocument>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.FileName).HasMaxLength(260);
                entity.Property(x => x.ContentType).HasMaxLength(128);
                entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(32);
            });

            builder.Entity<RagChunk>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Content).HasColumnType("nvarchar(max)");
                entity.Property(x => x.EmbeddingJson).HasColumnType("nvarchar(max)");
                entity.Property(x => x.ContentHash).HasMaxLength(128);
                entity.HasIndex(x => new { x.RagDocumentId, x.ChunkIndex }).IsUnique();
                entity.HasOne(x => x.RagDocument)
                    .WithMany(x => x.Chunks)
                    .HasForeignKey(x => x.RagDocumentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole() { Id = "1", Name = "student", NormalizedName = "STUDENT", ConcurrencyStamp = "1" },
                new IdentityRole() { Id = "2", Name = "teacher", NormalizedName = "TEACHER", ConcurrencyStamp = "2" },
                new IdentityRole() { Id = "3", Name = "parent", NormalizedName = "PARENT", ConcurrencyStamp = "3" }
                );
        }
    }
}
