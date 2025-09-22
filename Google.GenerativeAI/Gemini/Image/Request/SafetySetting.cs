namespace Google.GenerativeAI.Gemini.Image.Request
{
    using System.Text.Json.Serialization;

    public class SafetySetting
    {
        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("threshold")]
        public string Threshold { get; set; }
    }
}