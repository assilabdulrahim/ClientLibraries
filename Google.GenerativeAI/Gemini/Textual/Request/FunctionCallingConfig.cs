namespace Google.GenerativeAI.Gemini.Textual.Request

{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Configuration for the model's function calling behavior.
    /// </summary>
    public class FunctionCallingConfig
    {
        /// <summary>
        /// The mode for function calling. Can be "AUTO", "ANY", or "NONE".
        /// </summary>
        [JsonPropertyName("mode")]
        public string? Mode { get; set; }

        /// <summary>
        /// A list of function names to restrict the model to.
        /// </summary>
        [JsonPropertyName("allowedFunctionNames")]
        public List<string>? AllowedFunctionNames { get; set; }
    }
}