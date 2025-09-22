/// <summary>
/// Represents the full response payload from the Gemini API.
/// </summary>
namespace Google.GenerativeAI.Gemini.Textual.Response

{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Metadata on the token usage for the request and response.
    /// </summary>
    public class UsageMetadata
    {
        /// <summary>
        /// The number of tokens in the prompt.
        /// </summary>
        [JsonPropertyName("promptTokenCount")]
        public int PromptTokenCount { get; set; }

        /// <summary>
        /// The total number of tokens in all generated candidates.
        /// </summary>
        [JsonPropertyName("candidatesTokenCount")]
        public int CandidatesTokenCount { get; set; }

        /// <summary>
        /// The total number of tokens for the entire request.
        /// </summary>
        [JsonPropertyName("totalTokenCount")]
        public int TotalTokenCount { get; set; }
    }
}