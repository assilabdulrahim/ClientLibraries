namespace Google.GenerativeAI.Gemini.Image.Request
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class GenerationConfig
    {
        [JsonPropertyName("temperature")]
        public float? Temperature { get; set; }

        [JsonPropertyName("topP")]
        public float? TopP { get; set; }

        [JsonPropertyName("topK")]
        public int? TopK { get; set; }

        [JsonPropertyName("candidateCount")]
        public int? CandidateCount { get; set; }

        [JsonPropertyName("maxOutputTokens")]
        public int? MaxOutputTokens { get; set; }

        [JsonPropertyName("stopSequences")]
        public List<string> StopSequences { get; set; }

        [JsonPropertyName("responseMimeType")]
        public string ResponseMimeType { get; set; }
    }
}