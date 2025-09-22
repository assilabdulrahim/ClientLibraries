namespace Google.GenerativeAI.Gemini.Image.Request
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Represents a single part of a multimodal content message.
    /// Each Part can contain one type of data, such as text, image data, a file reference, or function-related information.
    /// </summary>
    public class Part
    {
        /// <summary>
        /// Gets or sets the text content. This is used for text-based prompts.
        /// Example: <code>new Part { Text = "Explain this image:" }</code>
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the inline media data, such as a base64-encoded image.
        /// This is used for sending media directly within the request body.
        /// Example: <code>new Part { InlineData = new InlineData { MimeType = "image/jpeg", Data = "..." } }</code>
        /// </summary>
        [JsonPropertyName("inlineData")]
        public InlineData InlineData { get; set; }

        /// <summary>
        /// Gets or sets a reference to a file stored in a cloud service (e.g., Google Cloud Storage).
        /// This is useful for large files that cannot be sent inline.
        /// Example: <code>new Part { FileData = new FileData { MimeType = "video/mp4", FileUri = "gs://my-bucket/video.mp4" } }</code>
        /// </summary>
        [JsonPropertyName("fileData")]
        public FileData FileData { get; set; }

        /// <summary>
        /// Gets or sets a function call requested by the model.
        /// This property is typically found in responses from the model, not in user requests.
        /// </summary>
        [JsonPropertyName("functionCall")]
        public FunctionCall FunctionCall { get; set; }

        /// <summary>
        /// Gets or sets the response from a function call that you executed.
        /// This is used to provide the result of a tool's execution back to the model.
        /// Example: <code>new Part { FunctionResponse = new FunctionResponse { Name = "get_weather", Response = new { temperature = 22, unit = "celsius" } } }</code>
        /// </summary>
        [JsonPropertyName("functionResponse")]
        public FunctionResponse FunctionResponse { get; set; }
    }
}