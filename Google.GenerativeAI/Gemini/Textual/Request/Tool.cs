namespace Google.GenerativeAI.Gemini.Textual.Request

{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A collection of tools the model can use.
    /// </summary>
    public class Tool
    {
        /// <summary>
        /// A list of function declarations.
        /// </summary>
        [JsonPropertyName("functionDeclarations")]
        public List<FunctionDeclaration>? FunctionDeclarations { get; set; }
    }
}