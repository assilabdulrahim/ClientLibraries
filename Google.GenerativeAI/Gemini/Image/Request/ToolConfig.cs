namespace Google.GenerativeAI.Gemini.Image.Request
{
    using System.Text.Json.Serialization;

    public class ToolConfig
    {
        [JsonPropertyName("functionCallingConfig")]
        public FunctionCallingConfig FunctionCallingConfig { get; set; }
    }
}