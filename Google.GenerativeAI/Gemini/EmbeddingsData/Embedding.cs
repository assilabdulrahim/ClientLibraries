namespace Google.GenerativeAI.Gemini.EmbeddingsData
{
    using System.Text.Json.Serialization;

    public class Embedding
    {
        [JsonPropertyName("values")]
        public float[] Values { get; set; }
    }
}