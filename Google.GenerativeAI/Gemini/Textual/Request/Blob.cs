namespace Google.GenerativeAI.Gemini.Textual.Request

{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Represents raw media bytes.
    /// </summary>
    public class Blob
    {
        /// <summary>
        /// The IANA standard MIME type of the media.
        /// </summary>
        [JsonPropertyName("mimeType")]
        public string MimeType { get; set; }

        /// <summary>
        /// The base64-encoded raw bytes of the media.
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}