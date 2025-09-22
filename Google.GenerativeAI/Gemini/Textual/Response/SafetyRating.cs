/// <summary>
/// Represents the full response payload from the Gemini API.
/// </summary>
namespace Google.GenerativeAI.Gemini.Textual.Response

{
    using System.Text.Json.Serialization;

    /// <summary>
    /// A safety rating for a specific harm category.
    /// </summary>
    public class SafetyRating
    {
        /// <summary>
        /// The harm category (e.g., "HARM_CATEGORY_SEXUALLY_EXPLICIT").
        /// </summary>
        [JsonPropertyName("category")]
        public string Category { get; set; }

        /// <summary>
        /// The probability that the content falls into this category (e.g., "NEGLIGIBLE", "LOW").
        /// </summary>
        [JsonPropertyName("probability")]
        public string Probability { get; set; }

        /// <summary>
        /// Indicates if the content was blocked for this reason.
        /// </summary>
        [JsonPropertyName("blocked")]
        public bool? Blocked { get; set; }
    }
}