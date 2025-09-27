namespace Google.GenerativeAI.Utility
{
    public static class Guard
    {
        /// <summary>
        /// Ensures that a list of embeddings is not null or empty.
        /// </summary>
        /// <param name="embeddings">The list of embeddings to check.</param>
        /// <param name="paramName">The name of the parameter being checked.</param>
        /// <exception cref="ArgumentException">Thrown if the list is null or empty.</exception>
        public static void AgainstNullOrEmpty(List<float[]> embeddings, string paramName)
        {
            if (embeddings == null || embeddings.Count == 0)
            {
                throw new ArgumentException("The list of embeddings cannot be null or empty.", paramName);
            }
        }

        public static void NotNull(object? obj, string paramName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName, "The parameter cannot be null.");
            }
        }
    }
}