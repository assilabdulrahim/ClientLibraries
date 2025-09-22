namespace Google.GenerativeAI.Gemini.Textual.Request

{
    using System.Text.Json.Serialization;

    /// <summary>
    /// A predicted function call to a declared tool.
    /// </summary>
    public class FunctionCall
    {
        /// <summary>
        /// The name of the function to call.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The arguments to the function, as a JSON object.
        /// </summary>
        [JsonPropertyName("args")]
        public object? Args { get; set; }
    }
}