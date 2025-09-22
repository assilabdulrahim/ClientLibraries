namespace Google.GenerativeAI.Gemini.Textual.Request

{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Defines a safety setting for a specific harm category.
    /// </summary>
    public class SafetySetting
    {
        /// <summary>
        /// Required. The harm category. (e.g., HARM_CATEGORY_HARASSMENT, HARM_CATEGORY_HATE_SPEECH).
        /// </summary>
        [JsonPropertyName("category")]
        public string Category { get; set; }

        /// <summary>
        /// Required. The blocking threshold. (e.g., BLOCK_NONE, BLOCK_LOW_AND_ABOVE).
        /// </summary>
        [JsonPropertyName("threshold")]
        public string Threshold { get; set; }
    }
}