namespace Google.GenerativeAI.Gemini.Image.Request
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class InstancePayload
    {
        [JsonPropertyName("contents")]
        public List<Content> Contents { get; set; }

        [JsonPropertyName("tools")]
        public List<Tool> Tools { get; set; }

        [JsonPropertyName("toolConfig")]
        public ToolConfig ToolConfig { get; set; }

        [JsonPropertyName("safetySettings")]
        public List<SafetySetting> SafetySettings { get; set; }

        [JsonPropertyName("generationConfig")]
        public GenerationConfig GenerationConfig { get; set; }

        [JsonPropertyName("systemInstruction")]
        public Content SystemInstruction { get; set; }
    }
}