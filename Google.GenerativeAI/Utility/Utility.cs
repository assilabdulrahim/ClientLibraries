namespace Google.GenerativeAI.Utility
{
    using System.Runtime.Serialization;

    public class Utility
    {
        /// <summary>
        /// Retrieves the EnumMember value of the specified enum.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>The EnumMember value as a string, or null if not found.</returns>
        public static string GetEnumMemberValue<T>(T enumValue) where T : Enum
        {
            var type = typeof(T);
            var memberInfo = type.GetMember(enumValue.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(EnumMemberAttribute), false);

            return attributes.Length > 0 ? ((EnumMemberAttribute)attributes[0]).Value : null;
        }

        /// <summary>
        /// Calculates the cosine similarity between two embedding vectors.
        /// Cosine similarity measures the cosine of the angle between two vectors.
        /// A value close to 1 indicates high similarity, 0 indicates orthogonality (no similarity),
        /// and -1 indicates complete dissimilarity (opposite directions).
        /// </summary>
        /// <param name="vector1">The first embedding vector (float array).</param>
        /// <param name="vector2">The second embedding vector (float array).</param>
        /// <returns>The cosine similarity score (float) between the two vectors,
        /// or 0.0 if either vector has zero magnitude (to prevent division by zero).</returns>
        /// <exception cref="ArgumentException">Thrown if the vectors have different lengths.</exception>
        public static float CalculateCosineSimilarity(float[] vector1, float[] vector2)
        {
            // Ensure both vectors have the same number of dimensions (length)
            if (vector1.Length != vector2.Length)
            {
                throw new ArgumentException("Vectors must have the same length to calculate cosine similarity.");
            }

            // Handle empty vectors or vectors with zero magnitude to avoid division by zero
            if (vector1.Length == 0)
            {
                return 0.0f;
            }

            float dotProduct = 0.0f;
            float magnitude1 = 0.0f;
            float magnitude2 = 0.0f;

            // Calculate the dot product (numerator) and the squared magnitudes (for the denominator)
            // Looping through each dimension to perform the calculations
            for (int i = 0; i < vector1.Length; i++)
            {
                dotProduct += vector1[i] * vector2[i]; // Sum of element-wise products
                magnitude1 += vector1[i] * vector1[i]; // Sum of squares for vector1
                magnitude2 += vector2[i] * vector2[i]; // Sum of squares for vector2
            }

            // Calculate the actual magnitudes (L2 norms) by taking the square root

            magnitude1 = (float)Math.Sqrt(magnitude1);
            magnitude2 = (float)Math.Sqrt(magnitude2);

            // If either vector has a magnitude of zero, their similarity is undefined or zero.
            // Returning 0.0 is a common practice in such cases to avoid NaN results.
            if (magnitude1 == 0.0 || magnitude2 == 0.0)
            {
                return 0.0f;
            }

            // Calculate the cosine similarity
            float similarity = dotProduct / (magnitude1 * magnitude2);

            return similarity;
        }

        /// <summary>
        /// Calculates the centroid (average vector) of a collection of embedding vectors.
        /// All vectors in the collection must have the same length.
        /// </summary>
        /// <param name="embeddings">A list of embedding vectors (float arrays).</param>
        /// <returns>A new float array representing the centroid vector.</returns>
        /// <exception cref="ArgumentException">Thrown if the list is empty or vectors have inconsistent lengths.</exception>
        public static float[] CalculateCentroid(List<float[]> embeddings)
        {
            if (embeddings == null || embeddings.Count == 0)
            {
                throw new ArgumentException("The list of embeddings cannot be null or empty.");
            }

            int dimension = embeddings[0].Length;
            float[] centroid = new float[dimension];

            // Sum all vectors element-wise
            foreach (float[] embedding in embeddings)
            {
                if (embedding.Length != dimension)
                {
                    throw new ArgumentException("All embedding vectors must have the same dimension.");
                }

                for (int i = 0; i < dimension; i++)
                {
                    centroid[i] += embedding[i];
                }
            }

            // Divide by the number of embeddings to get the average
            for (int i = 0; i < dimension; i++)
            {
                centroid[i] /= embeddings.Count;
            }

            return centroid;
        }

        /// <summary>
        /// Finds the nearest neighbor to a query embedding from a list of candidate embeddings
        /// based on cosine similarity.
        /// </summary>
        /// <param name="queryEmbedding">The embedding vector for which to find the nearest neighbor.</param>
        /// <param name="candidateEmbeddings">A list of embedding vectors to search through.</param>
        /// <returns>A tuple containing the nearest neighbor embedding (float[]) and its similarity score,
        /// or null if the candidate list is empty.</returns>
        public static Tuple<float[], float>? FindNearestNeighbor(float[] queryEmbedding, List<float[]> candidateEmbeddings)
        {
            if (candidateEmbeddings == null || candidateEmbeddings.Count == 0)
            {
                return null; // No candidates to compare against
            }

            float maxSimilarity = -1.0f; // Cosine similarity ranges from -1 to 1
            float[] nearestNeighbor = null;

            foreach (float[] candidate in candidateEmbeddings)
            {
                float currentSimilarity = CalculateCosineSimilarity(queryEmbedding, candidate);

                if (currentSimilarity > maxSimilarity)
                {
                    maxSimilarity = currentSimilarity;
                    nearestNeighbor = candidate;
                }
            }

            return new Tuple<float[], float>(nearestNeighbor, maxSimilarity);
        }

        public class TextPassage
        {
            public string OriginalText { get; set; }
            public float[] Embedding { get; set; }

            public TextPassage(string text, float[] embedding)
            {
                OriginalText = text;
                Embedding = embedding;
            }
        }

        /// <summary>
        /// Finds the best matching passage for a given query embedding from a list of candidate passages.
        /// The best match is determined by the highest cosine similarity score.
        /// </summary>
        /// <param name="queryEmbedding">The embedding vector of the search query.</param>
        /// <param name="candidatePassages">A list of TextPassage objects to search through.</param>
        /// <returns>The TextPassage object with the highest similarity to the query,
        /// or null if the candidatePassages list is empty.</returns>
        public static TextPassage FindBestPassage(float[] queryEmbedding, List<TextPassage> candidatePassages)
        {
            if (candidatePassages == null || candidatePassages.Count == 0)
            {
                return null;
            }

            TextPassage bestMatch = null;
            float highestSimilarity = -1.0f; // Initialize with the lowest possible similarity

            foreach (TextPassage passage in candidatePassages)
            {
                // Ensure the passage embedding is not null before calculating similarity
                if (passage.Embedding == null)
                {
                    Console.WriteLine($"Warning: Skipping passage with null embedding: '{passage.OriginalText}'");
                    continue;
                }

                float currentSimilarity = CalculateCosineSimilarity(queryEmbedding, passage.Embedding);

                if (currentSimilarity > highestSimilarity)
                {
                    highestSimilarity = currentSimilarity;
                    bestMatch = passage;
                }
            }

            return bestMatch;
        }

        /// <summary>
        /// Calculates the dot product of two embedding vectors.
        /// The dot product is the sum of the products of their corresponding components.
        /// </summary>
        /// <param name="vector1">The first embedding vector (float array).</param>
        /// <param name="vector2">The second embedding vector (float array).</param>
        /// <returns>The dot product (float) of the two vectors.</returns>
        /// <exception cref="ArgumentException">Thrown if the vectors have different lengths.</exception>
        public static float DotProduct(float[] vector1, float[] vector2)
        {
            if (vector1.Length != vector2.Length)
            {
                throw new ArgumentException("Vectors must have the same length to calculate dot product.");
            }

            float dotProduct = 0.0f;
            for (int i = 0; i < vector1.Length; i++)
            {
                dotProduct += vector1[i] * vector2[i];
            }
            return dotProduct;
        }
    }
}