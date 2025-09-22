namespace Google.GenerativeAI.Gemini.Textual.Request

{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Configuration options for controlling the model's generation process.
    /// </summary>
    public class GenerationConfig
    {
        /// <summary>
        /// A list of sequences that will stop generation.
        /// </summary>
        [JsonPropertyName("stopSequences")]
        public List<string>? StopSequences { get; set; }

        /// <summary>
        /// The number of generated responses to return.
        /// </summary>
        [JsonPropertyName("candidateCount")]
        public int? CandidateCount { get; set; }

        /// <summary>
        /// The maximum number of tokens to include in a candidate.
        /// </summary>
        [JsonPropertyName("maxOutputTokens")]
        public int? MaxOutputTokens { get; set; }

        /// <summary>
        /// Controls the randomness of the output (0.0-1.0).
        /// </summary>
        [JsonPropertyName("temperature")]
        public float? Temperature { get; set; }

        /// <summary>
        /// The cumulative probability of tokens to consider when sampling.
        /// </summary>
        [JsonPropertyName("topP")]
        public float? TopP { get; set; }

        /// <summary>
        /// The maximum number of tokens to consider when sampling.
        /// </summary>
        [JsonPropertyName("topK")]
        public int? TopK { get; set; }
    }
}