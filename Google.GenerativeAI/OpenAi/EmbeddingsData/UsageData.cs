namespace Google.GenerativeAI.OpenAi.EmbeddingsData
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Represents the token usage statistics for an API request.
    /// </summary>
    public class UsageData
    {
        /// <summary>
        /// The number of tokens in the input prompt.
        /// </summary>
        [JsonPropertyName("prompt_tokens")]
        public int PromptTokens { get; set; }

        /// <summary>
        /// The total number of tokens used in the request (prompt + completion).
        /// For embeddings, this is the same as prompt_tokens.
        /// </summary>
        [JsonPropertyName("total_tokens")]
        public int TotalTokens { get; set; }
    }

}
