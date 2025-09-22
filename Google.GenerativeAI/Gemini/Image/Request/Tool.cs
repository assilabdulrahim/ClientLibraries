namespace Google.GenerativeAI.Gemini.Image.Request
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class Tool
    {
        [JsonPropertyName("functionDeclarations")]
        public List<FunctionDeclaration> FunctionDeclarations { get; set; }

        [JsonPropertyName("codeExecution")]
        public CodeExecution CodeExecution { get; set; }
    }
}