namespace Google.GenerativeAI.Gemini.Textual.Request

{
    using System.Text.Json.Serialization;

    /// <summary>
    /// A declaration for a function that the model can call.
    /// </summary>
    public class FunctionDeclaration
    {
        /// <summary>
        /// The name of the function.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// A description of what the function does.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The parameters of the function, described as a JSON schema object.
        /// </summary>
        [JsonPropertyName("parameters")]
        public Schema? Parameters { get; set; } // Represented as a JSON Schema object
    }
}