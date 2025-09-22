/// <summary>
/// Represents the full response payload from the Gemini API.
/// </summary>
namespace Google.GenerativeAI.Gemini.Textual.Response

{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Citation information if the model's response is based on external sources.
    /// </summary>
    public class CitationMetadata
    {
        /// <summary>
        /// A list of sources that the model cited in its response.
        /// </summary>
        [JsonPropertyName("citationSources")]
        public List<CitationSource> CitationSources { get; set; }
    }
}