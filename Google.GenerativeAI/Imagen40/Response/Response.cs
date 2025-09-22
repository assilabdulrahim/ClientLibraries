namespace Google.GenerativeAI.Imagen40.Response
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Represents the root object of the response from the Imagen API.
    /// </summary>
    public class ImagenGenerateResponse
    {
        /// <summary>
        /// A list of prediction results, where each result contains one generated image
        /// and its associated metadata.
        /// </summary>
        [JsonPropertyName("predictions")]
        public List<Prediction> Predictions { get; set; }
    }

    /// <summary>
    /// Represents a single generated image and its associated metadata.
    /// </summary>
    public class Prediction
    {
        /// <summary>
        /// The generated image encoded as a Base64 string.
        /// This property is populated if the request's output_format was "b64_json".
        /// </summary>
        [JsonPropertyName("bytesBase64Encoded")]
        public string? BytesBase64Encoded { get; set; }

        /// <summary>
        /// A signed URL pointing to the generated image file in a temporary bucket.
        /// This property is populated if the request's output_format was "url".
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        /// <summary>
        /// A list of safety ratings for the generated image, categorized by potential harm.
        /// </summary>
        [JsonPropertyName("safety_ratings")]
        public List<SafetyRating> SafetyRatings { get; set; }

        /// <summary>
        /// Contains metadata about the generation process for this specific image.
        /// </summary>
        [JsonPropertyName("generation_info")]
        public GenerationInfo GenerationInfo { get; set; }
    }

    /// <summary>
    /// Details a safety attribute assessment for a generated image.
    /// </summary>
    public class SafetyRating
    {
        /// <summary>
        /// The safety category being evaluated (e.g., "Violent", "Hate Speech").
        /// </summary>
        [JsonPropertyName("category")]
        public string Category { get; set; }

        /// <summary>
        /// The model's confidence score that the image belongs to this category.
        /// </summary>
        [JsonPropertyName("probability_score")]
        public float ProbabilityScore { get; set; }

        /// <summary>
        /// The predicted severity score if the image is flagged for this category.
        /// </summary>
        [JsonPropertyName("severity_score")]
        public float SeverityScore { get; set; }

        /// <summary>
        /// Indicates whether the image was blocked due to this safety category violation.
        /// </summary>
        [JsonPropertyName("blocked")]
        public bool Blocked { get; set; }
    }

    /// <summary>
    /// Contains metadata about the image generation process.
    /// </summary>
    public class GenerationInfo
    {
        /// <summary>
        /// The seed value that was used to generate this specific image.
        /// </summary>
        [JsonPropertyName("seed")]
        public int Seed { get; set; }
    }
}