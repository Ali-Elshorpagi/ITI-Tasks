namespace Lab04.Configurations
{
    public class RagOptions
    {
        public string UploadRoot { get; set; } = "Uploads";
        public int ChunkSizeWords { get; set; } = 220;
        public int ChunkOverlapWords { get; set; } = 40;
        public int DefaultTopK { get; set; } = 8;
        public int DefaultContextChunks { get; set; } = 5;
    }
}
