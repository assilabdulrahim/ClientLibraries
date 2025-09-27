namespace Google.GenerativeAI.Gemini.GenerativeData
{
    using System.Text.Json.Serialization;

    public class GeminiGenerationConfig
    {
        [JsonPropertyName("stopSequences")]
        public List<string> StopSequences { get; set; }

        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }

        [JsonPropertyName("topP")]
        public double TopP { get; set; }

        [JsonPropertyName("topK")]
        public int TopK { get; set; }
    }
}