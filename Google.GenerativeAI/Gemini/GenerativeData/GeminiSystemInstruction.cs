namespace Google.GenerativeAI.Gemini.GenerativeData
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class GeminiSystemInstruction
    {
        [JsonPropertyName("parts")]
        public List<GeminiPart> Parts { get; set; }
    }
}