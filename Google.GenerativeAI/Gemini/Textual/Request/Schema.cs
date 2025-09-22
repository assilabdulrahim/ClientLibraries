namespace Google.GenerativeAI.Gemini.Textual.Request
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class Schema
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("enum")]
        public List<string> Enum { get; set; }

        [JsonPropertyName("format")]
        public string Format { get; set; }

        [JsonPropertyName("items")]
        public Schema Items { get; set; }

        [JsonPropertyName("nullable")]
        public bool? Nullable { get; set; }

        [JsonPropertyName("properties")]
        public Dictionary<string, Schema> Properties { get; set; }

        [JsonPropertyName("required")]
        public List<string> Required { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}