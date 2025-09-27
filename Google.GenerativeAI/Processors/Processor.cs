namespace Google.GenerativeAI.Processors
{
    using Google.GenerativeAI.Contracts;
    using Google.GenerativeAI.Enums;
    using Google.GenerativeAI.Settings;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class Processor : EmbeddingsProcessor, IProcessor
    {
        private readonly string apiKey;

        public Processor(IHttpClientFactory httpClientFactory, string apiKey, ILogger<EmbeddingsProcessor> logger, IOptions<AiClientSettings> options)
            : base(httpClientFactory, logger, options)
        {
            this.apiKey = apiKey;
        }

        /// <summary>
        /// Asynchronously retrieves the embedding vector for the specified text.
        /// </summary>
        /// <param name="text">The input text to generate the embedding for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the embedding vector as a float array,
        /// or null if the embedding could not be generated.
        /// </returns>
        public async Task<float[]?> GetDocumentEmbeddingsAsync(string text)
        {
            return await GetEmbedingVectorAsync(apiKey, text, EmbeddingTaskType.RetrievalDocument);
        }

        public async Task<float[]?> GetQuerymbeddingsAsync(string text)
        {
            return await GetEmbedingVectorAsync(apiKey, text, EmbeddingTaskType.QuestionAnswering);
        }

        public async Task<float[]?> GetSimilaritymbeddingsAsync(string text)
        {
            return await GetEmbedingVectorAsync(apiKey, text, EmbeddingTaskType.SemanticSimilarity);
        }
    }
}