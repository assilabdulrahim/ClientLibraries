namespace Google.GenerativeAI.Gemini.Image.Request
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class FunctionCallingConfig
    {
        [JsonPropertyName("mode")]
        public string Mode { get; set; }

        [JsonPropertyName("allowedFunctionNames")]
        public List<string> AllowedFunctionNames { get; set; }
    }
}