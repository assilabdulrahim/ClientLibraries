/// <summary>
/// Represents the full response payload from the Gemini API.
/// </summary>
namespace Google.GenerativeAI.Gemini.Textual.Response

{
    using System.Text.Json.Serialization;

    /// <summary>
    /// A single cited source.
    /// </summary>
    public class CitationSource
    {
        /// <summary>
        /// The starting index of the cited content in the response.
        /// </summary>
        [JsonPropertyName("startIndex")]
        public int? StartIndex { get; set; }

        /// <summary>
        /// The ending index of the cited content in the response.
        /// </summary>
        [JsonPropertyName("endIndex")]
        public int? EndIndex { get; set; }

        /// <summary>
        /// The URI of the cited source.
        /// </summary>
        [JsonPropertyName("uri")]
        public string? Uri { get; set; }

        /// <summary>
        /// The license of the cited source.
        /// </summary>
        [JsonPropertyName("license")]
        public string? License { get; set; }
    }
}