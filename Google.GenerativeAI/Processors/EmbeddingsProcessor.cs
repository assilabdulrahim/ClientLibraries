namespace Google.GenerativeAI.Processors
{
    using Ai.Fluent.Core.DataContracts;
    using Google.GenerativeAI.Enums;
    using Google.GenerativeAI.Gemini.EmbeddingsData;
    using Google.GenerativeAI.Settings;
    using Google.GenerativeAI.Utility;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Polly;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;

    public class EmbeddingsProcessor
    {
        private const string ApiKeyHeader = "x-goog-api-key";
        private const string ContentType = "application/json";
        private static HttpClient Client;
        private readonly ILogger<EmbeddingsProcessor> Logger;
        private readonly AiClientSettings Options;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddingsProcessor"/> class.
        /// </summary>
        /// <param name="httpClientFactory">
        /// The <see cref="IHttpClientFactory"/> used to create <see cref="HttpClient"/> instances for API requests.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger{EmbeddingsProcessor}"/> instance for logging diagnostic and error information.
        /// </param>
        /// <param name="options">
        /// The <see cref="IOptions{AiClientSettings}"/> containing configuration settings for the AI client.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="httpClientFactory"/> or <paramref name="logger"/> is <c>null</c>.
        /// </exception>
        protected EmbeddingsProcessor(
        IHttpClientFactory httpClientFactory,
        ILogger<EmbeddingsProcessor> logger,
        IOptions<AiClientSettings> options)
        {
            Guard.NotNull(httpClientFactory, nameof(httpClientFactory));
            Guard.NotNull(logger, nameof(logger));
            Guard.NotNull(options, nameof(options));
            Guard.NotNull(options.Value, $"{nameof(options)}.{nameof(options.Value)}");

            Client = httpClientFactory.CreateClient();
            Logger = logger;
            Options = options.Value;
        }

        private async Task<string> GetEmbeddingsAsync(
            string apiKey,
            string url,
            string[] texts,
            EmbeddingTaskType taskType,
            ILogger logger,
            int maxPayloadSize,
            int outputDimensionality)
        {
            var payload = new Payload
            {
                Content = new Payload.ContentItem
                {
                    Parts = texts.Select(text => new Payload.Part { Text = text }).ToArray()
                },
                TaskType = Utility.GetEnumMemberValue(taskType),
                OutputDimensionality = outputDimensionality
            };

            var json = JsonSerializer.Serialize(payload);
            int totalBytes = Encoding.UTF8.GetByteCount(json);
            logger.LogInformation("Embedding request payload size: {Size} bytes", totalBytes);

            if (totalBytes > maxPayloadSize)
            {
                logger.LogError("Embedding request payload exceeds the {MaxSize} byte limit. Actual size: {Size} bytes.", maxPayloadSize, totalBytes);
                throw new ArgumentException($"Embedding request payload exceeds the {maxPayloadSize} byte limit. Actual size: {totalBytes} bytes.");
            }

            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Add(ApiKeyHeader, apiKey);

            using var content = new StringContent(json, Encoding.UTF8, ContentType);

            // Polly retry policy for 429 Too Many Requests with exponential backoff
            var retryPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => r.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(
                    retryCount: 5,
                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)), // 2, 4, 8, 16, 32 seconds
                    onRetry: (outcome, timespan, retryAttempt, context) =>
                    {
                        logger.LogWarning("Received HTTP 429 Too Many Requests. Retry {RetryAttempt} in {Delay}s.", retryAttempt, timespan.TotalSeconds);
                    });

            try
            {
                HttpResponseMessage response = await retryPolicy.ExecuteAsync(async () =>
                {
                    return await Client.PostAsync(url, content);
                });

                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    logger.LogError("Embedding API error: {StatusCode} - {Response}", response.StatusCode, responseBody);
                }
                else
                {
                    logger.LogInformation("Embedding API success: {StatusCode}", response.StatusCode);
                }

                return responseBody;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception occurred while calling Embedding API.");
                throw;
            }
        }

        protected async Task<float[]?> GetEmbedingVectorAsync(string apiKey, string text, EmbeddingTaskType taskType)
        {
            var responseBody = await GetEmbeddingsAsync(
                apiKey,
                Options.EmbeddingUrl,
                new[] { text },
                taskType,
                Logger,
                Options.MaxPayloadSize,
                Options.OutputDimensionality);

            try
            {
                var embeddingResponse = JsonSerializer.Deserialize<EmbeddingResponse>(responseBody);
                var embeddings = embeddingResponse.Embedding?.Values;

                if (embeddings != null)
                {
                    Logger.LogInformation("Successfully parsed embedding vector. Length: {Length}", embeddings.Length);
                }
                else
                {
                    Logger.LogWarning("Embedding vector is null or missing in the response.");
                }

                return embeddings;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to parse embedding response: {ResponseBody}", responseBody);
                throw;
            }
        }
    }
}