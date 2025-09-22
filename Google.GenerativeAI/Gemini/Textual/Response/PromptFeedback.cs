/// <summary>
/// Represents the full response payload from the Gemini API.
/// </summary>
namespace Google.GenerativeAI.Gemini.Textual.Response

{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Feedback on the safety of the prompt sent to the model.
    /// </summary>
    public class PromptFeedback
    {
        /// <summary>
        /// The reason the prompt was blocked, if applicable.
        /// </summary>
        [JsonPropertyName("blockReason")]
        public string? BlockReason { get; set; }

        /// <summary>
        /// A list of safety ratings for the prompt.
        /// </summary>
        [JsonPropertyName("safetyRatings")]
        public List<SafetyRating> SafetyRatings { get; set; }
    }
}