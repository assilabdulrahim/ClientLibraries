namespace Google.GenerativeAI.Gemini.Image.Request
{
    using System.Text.Json.Serialization;

    public class InlineData
    {
        [JsonPropertyName("mimeType")]
        public string MimeType { get; set; }

        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}