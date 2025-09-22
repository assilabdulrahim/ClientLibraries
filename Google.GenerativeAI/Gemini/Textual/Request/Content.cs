namespace Google.GenerativeAI.Gemini.Textual.Request

{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A single content block in the conversation, composed of one or more parts.
    /// </summary>
    public class Content
    {
        /// <summary>
        /// Required. An ordered list of parts that make up this content block.
        /// </summary>
        [JsonPropertyName("parts")]
        public List<Part> Parts { get; set; }

        /// <summary>
        /// Optional. The role of the author of this content (e.g., "user", "model", "tool").
        /// </summary>
        [JsonPropertyName("role")]
        public string? Role { get; set; }
    }
}