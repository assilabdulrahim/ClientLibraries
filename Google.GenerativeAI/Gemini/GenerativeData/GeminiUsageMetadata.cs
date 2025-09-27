namespace Google.GenerativeAI.Gemini.GenerativeData
{
    using System.Text.Json.Serialization;

    public class GeminiUsageMetadata
    {
        [JsonPropertyName("promptTokenCount")]
        public int PromptTokenCount { get; set; }

        [JsonPropertyName("candidatesTokenCount")]
        public int CandidatesTokenCount { get; set; }

        [JsonPropertyName("totalTokenCount")]
        public int TotalTokenCount { get; set; }

        [JsonPropertyName("promptTokensDetails")]
        public List<GeminiPromptTokensDetail> PromptTokensDetails { get; set; }

        [JsonPropertyName("thoughtsTokenCount")]
        public int ThoughtsTokenCount { get; set; }
    }
}