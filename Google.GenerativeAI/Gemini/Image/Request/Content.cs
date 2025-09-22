namespace Google.GenerativeAI.Gemini.Image.Request
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class Content
    {
        [JsonPropertyName("parts")]
        public List<Part> Parts { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }
    }
}