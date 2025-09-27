namespace Google.GenerativeAI.Gemini.GenerativeData
{
    using System.Text.Json.Serialization;

    public class GeminiCandidateContent
    {
        [JsonPropertyName("parts")]
        public List<GeminiPart> Parts { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }
    }
}