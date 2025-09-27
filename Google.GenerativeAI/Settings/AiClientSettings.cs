namespace Google.GenerativeAI.Settings
{
    public class AiClientSettings
    {
        public string ApiKey { get; set; }
        public int DefaultAppId { get; set; }
        public string DefaultCategory { get; set; }
        public string EmbeddingUrl { get; set; } = "https://generativelanguage.googleapis.com/v1beta/models/gemini-embedding-001:embedContent";
        public string LlmUrl { get; set; }
        public int MaxPayloadSize { get; set; } = 36000;
        public int OutputDimensionality { get; set; } = 768;

    }
}