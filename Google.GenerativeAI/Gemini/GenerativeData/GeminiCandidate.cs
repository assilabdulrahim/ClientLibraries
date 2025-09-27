namespace Google.GenerativeAI.Gemini.GenerativeData
{
    using System.Text.Json.Serialization;

    public class GeminiCandidate
    {
        [JsonPropertyName("content")]
        public GeminiCandidateContent Content { get; set; }

        [JsonPropertyName("finishReason")]
        public string FinishReason { get; set; }

        [JsonPropertyName("index")]
        public int Index { get; set; }
    }
}