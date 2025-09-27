namespace Google.GenerativeAI.Contracts
{
    /// <summary>
    /// Provides methods for processing text and generating document embeddings.
    /// </summary>
    public interface IProcessor
    {
        /// <summary>
        /// Generates an embedding vector for the specified text document.
        /// </summary>
        /// <param name="text">The input text to process and embed.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the embedding vector as a float array,
        /// or <c>null</c> if the embedding could not be generated.
        /// </returns>
        Task<float[]?> GetDocumentEmbeddingsAsync(string text);

        Task<float[]?> GetQuerymbeddingsAsync(string text);

        Task<float[]?> GetSimilaritymbeddingsAsync(string text);
    }
}