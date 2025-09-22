namespace Google.GenerativeAI.Gemini.Image.Request
{
    using System.Text.Json.Serialization;

    public class FileData
    {
        [JsonPropertyName("mimeType")]
        public string MimeType { get; set; }

        [JsonPropertyName("fileUri")]
        public string FileUri { get; set; }
    }
}