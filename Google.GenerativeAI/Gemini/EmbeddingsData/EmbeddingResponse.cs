namespace Google.GenerativeAI.Gemini.EmbeddingsData
{
    using System.Text.Json.Serialization;

    public class EmbeddingResponse
    {
        [JsonPropertyName("embedding")]
        public Embedding Embedding { get; set; }
    }
}