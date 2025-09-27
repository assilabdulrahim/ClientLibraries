namespace Google.GenerativeAI.Gemini.GenerativeData
{
    using System.Text.Json.Serialization;

    public class GeminiRequest
    {
        [JsonPropertyName("system_instruction")]
        public GeminiSystemInstruction SystemInstruction { get; set; }

        [JsonPropertyName("contents")]
        public List<GeminiContent> Contents { get; set; }
    }
}