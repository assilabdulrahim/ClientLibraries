namespace Google.GenerativeAI.OpenAi.EmbeddingsData
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Represents the top-level response object from the Azure OpenAI Embeddings API.
    /// </summary>
    public class EmbeddingResponse
    {
        /// <summary>
        /// The type of object, typically "list".
        /// </summary>
        [JsonPropertyName("object")]
        public string ObjectType { get; set; }

        /// <summary>
        /// A list of embedding data objects, one for each input string.
        /// </summary>
        [JsonPropertyName("data")]
        public List<EmbeddingData> Data { get; set; }

        /// <summary>
        /// The name of the model used to generate the embeddings.
        /// </summary>
        [JsonPropertyName("model")]
        public string Model { get; set; }

        /// <summary>
        /// Information about the token usage for the request.
        /// </summary>
        [JsonPropertyName("usage")]
        public UsageData Usage { get; set; }
    }

}
