namespace Google.GenerativeAI.Gemini.EmbeddingsData
{
    using System.Text.Json.Serialization;

    public class EmbeddingConfig
    {
        [JsonPropertyName("output_dimensionality")]
        public int OutputDimensionality { get; set; }

        // Add other config properties if needed
    }
}