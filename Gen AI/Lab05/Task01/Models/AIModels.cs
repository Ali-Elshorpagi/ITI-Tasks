using Newtonsoft.Json;
using System.Collections.Generic;

namespace Task01.Models
{
    public class QuestionRequest
    {
        public string? Question { get; set; }
    }

    public class AIResponse
    {
        public string? Answer { get; set; }
    }

    public class N8nResponseItem
    {
        [JsonProperty("output")]
        public List<N8nOutput>? Output { get; set; }
    }

    public class N8nOutput
    {
        [JsonProperty("content")]
        public List<N8nContent>? Content { get; set; }
    }

    public class N8nContent
    {
        [JsonProperty("text")]
        public string? Text { get; set; }
    }
}
