namespace Google.GenerativeAI.Gemini.Image.Request
{
    using System.Text.Json.Serialization;

    public class FunctionCall
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("args")]
        public object Args { get; set; }
    }
}