namespace Google.GenerativeAI.Gemini.Textual.Request

{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Represents the complete request payload sent to the Gemini API's generateContent method.
    /// </summary>
    public class GeminiRequest
    {
        /// <summary>
        /// Required. The content of the current conversation with the model.
        /// </summary>
        [JsonPropertyName("contents")]
        public List<Content> Contents { get; set; }

        /// <summary>
        /// Optional. A list of tools the model may call to generate the next response.
        /// </summary>
        [JsonPropertyName("tools")]
        public List<Tool>? Tools { get; set; }

        /// <summary>
        /// Optional. Tool configuration parameters for function calling.
        /// </summary>
        [JsonPropertyName("toolConfig")]
        public ToolConfig? ToolConfig { get; set; }

        /// <summary>
        /// Optional. A list of unique safety settings for blocking unsafe content.
        /// </summary>
        [JsonPropertyName("safetySettings")]
        public List<SafetySetting>? SafetySettings { get; set; }

        /// <summary>
        /// Optional. Configuration options for model generation and outputs.
        /// </summary>
        [JsonPropertyName("generationConfig")]
        public GenerationConfig? GenerationConfig { get; set; }

        /// <summary>
        /// Optional. Developer-set system instruction. Currently, only text is supported.
        /// </summary>
        [JsonPropertyName("systemInstruction")]
        public Content? SystemInstruction { get; set; }
    }
}