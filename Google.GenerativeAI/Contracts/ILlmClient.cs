namespace Google.GenerativeAI.Contracts
{
    /// <summary>
    /// Represents a client interface for interacting with a Large Language Model (LLM).
    /// </summary>
    public interface ILlmClient
    {
        /// <summary>
        /// Asynchronously retrieves expanded concepts based on the provided prompt.
        /// </summary>
        /// <param name="prompt">The input prompt to generate expanded concepts from.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the expanded concepts as a string, or null if no concepts are generated.</returns>
        Task<string?> GetExpandedConceptsAsync(string prompt);
    }
}