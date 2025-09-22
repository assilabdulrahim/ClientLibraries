namespace Google.GenerativeAI.Gemini.Textual.Request

{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Configuration for how the model should use tools.
    /// </summary>
    public class ToolConfig
    {
        /// <summary>
        /// Configuration for function calling.
        /// </summary>
        [JsonPropertyName("functionCallingConfig")]
        public FunctionCallingConfig? FunctionCallingConfig { get; set; }
    }
}