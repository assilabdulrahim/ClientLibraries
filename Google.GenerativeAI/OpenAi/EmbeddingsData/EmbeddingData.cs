namespace Google.GenerativeAI.OpenAi.EmbeddingsData
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Represents the embedding vector and its metadata for a single input.
    /// </summary>
    public class EmbeddingData
    {
        /// <summary>
        /// The type of object, typically "embedding".
        /// </summary>
        [JsonPropertyName("object")]
        public string ObjectType { get; set; }

        /// <summary>
        /// The embedding vector, which is a list of floating-point numbers.
        /// </summary>
        [JsonPropertyName("embedding")]
        public List<double> Embedding { get; set; }

        /// <summary>
        /// The index of the input string this embedding corresponds to.
        /// </summary>
        [JsonPropertyName("index")]
        public int Index { get; set; }
    }

}
