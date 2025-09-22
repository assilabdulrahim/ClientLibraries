namespace Google.GenerativeAI.Gemini.Image.Request
{
    using System.Text.Json.Serialization;

    public class FunctionDeclaration
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("parameters")]
        public Schema Parameters { get; set; }
    }
}