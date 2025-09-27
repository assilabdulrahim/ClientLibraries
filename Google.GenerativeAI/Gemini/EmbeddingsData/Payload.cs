namespace Ai.Fluent.Core.DataContracts
{
    using System.Text.Json.Serialization;

    public partial class Payload
    {
        [JsonPropertyName("content")]
        public ContentItem Content { get; set; }

        [JsonPropertyName("taskType")]
        public string TaskType { get; set; } // Remove if not needed by API

        [JsonPropertyName("outputDimensionality")]
        public int OutputDimensionality { get; set; }

        //[JsonPropertyName("embedding_config")]
        //public EmbeddingConfig EmbeddingConfig { get; set; }

        public class ContentItem
        {
            [JsonPropertyName("parts")]
            public Part[] Parts { get; set; }
        }
    }
}