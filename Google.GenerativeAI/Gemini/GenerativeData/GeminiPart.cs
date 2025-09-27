namespace Google.GenerativeAI.Gemini.GenerativeData
{
    using System.Text.Json.Serialization;

    public class GeminiPart
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}