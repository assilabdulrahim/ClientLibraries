namespace Google.GenerativeAI.Imagen40.Request
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Represents the request payload for the Imagen text-to-image generation model.
    /// </summary>
    public class ImagenGenerateRequest
    {
        /// <summary>
        /// Required. The main text description of the image to generate.
        /// This prompt details the subject, style, colors, and composition.
        /// Example: "A photorealistic portrait of an astronaut riding a horse on Mars."
        /// </summary>
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; }

        /// <summary>
        /// Optional. A text description of elements to exclude from the generated image.
        /// This helps guide the model away from unwanted features.
        /// Example: "poorly rendered, blurry, text, watermark, signature."
        /// </summary>
        [JsonPropertyName("negative_prompt")]
        public string? NegativePrompt { get; set; }

        /// <summary>
        /// Optional. A number between 0 and 2,147,483,647. Using the same seed with the
        /// same prompt will produce similar images, aiding reproducibility.
        /// If not specified, a random seed is used.
        /// </summary>
        [JsonPropertyName("seed")]
        public int? Seed { get; set; }

        /// <summary>
        /// Optional. The number of images to generate for the prompt.
        /// Value must be between 1 and 4. Defaults to 1 if not specified.
        /// </summary>
        [JsonPropertyName("sample_count")]
        public int? SampleCount { get; set; }

        /// <summary>
        /// Optional. The aspect ratio of the generated image.
        /// Accepted values are "1:1" (square), "9:16" (portrait), and "16:9" (landscape).
        /// Defaults to "1:1".
        /// </summary>
        [JsonPropertyName("aspect_ratio")]
        public string? AspectRatio { get; set; }

        /// <summary>
        /// Optional. The format for the output image data.
        /// Accepted values: "b64_json" (Base64-encoded string) or "url".
        /// Defaults to "b64_json". A "url" is valid for 30 days.
        /// </summary>
        [JsonPropertyName("output_format")]
        public string? OutputFormat { get; set; }

        /// <summary>
        /// Optional. The strength of the safety filter.
        /// Accepted values: "block_most", "block_some", "block_few".
        /// Defaults to blocking content with a medium or higher probability of being unsafe.
        /// </summary>
        [JsonPropertyName("safety_filter_level")]
        public string? SafetyFilterLevel { get; set; }

        /// <summary>
        /// Optional. Controls the generation of people in images.
        /// Accepted values: "allow_adult" (default), "dont_allow".
        /// Use "dont_allow" to help prevent the generation of photorealistic people.
        /// </summary>
        [JsonPropertyName("person_generation")]
        public string? PersonGeneration { get; set; }

        /// <summary>
        /// Optional. If set to true, a Google DeepMind watermark is added to the image.
        /// Defaults to true.
        /// </summary>
        [JsonPropertyName("add_watermark")]
        public bool? AddWatermark { get; set; }
    }
}