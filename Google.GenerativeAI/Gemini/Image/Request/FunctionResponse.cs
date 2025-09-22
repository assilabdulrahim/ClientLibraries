namespace Google.GenerativeAI.Gemini.Image.Request
{
    using System.Text.Json.Serialization;

    public class FunctionResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("response")]
        public object Response { get; set; }
    }
}