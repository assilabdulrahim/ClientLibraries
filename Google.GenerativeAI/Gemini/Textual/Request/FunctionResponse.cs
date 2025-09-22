namespace Google.GenerativeAI.Gemini.Textual.Request

{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The result of a function call, to be sent back to the model.
    /// </summary>
    public class FunctionResponse
    {
        /// <summary>
        /// The name of the function that was called.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The output of the function, as a JSON object.
        /// </summary>
        [JsonPropertyName("response")]
        public object Response { get; set; }
    }
}