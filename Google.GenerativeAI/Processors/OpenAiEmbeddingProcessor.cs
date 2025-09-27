namespace Google.GenerativeAI.Processors
{
    using Google.GenerativeAI.OpenAi.EmbeddingsData;
    using Microsoft.Extensions.Options;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class OpenAiEmbeddingProcessor
    {
        private readonly OpenAiEmbeddingOptions Options;
        private readonly HttpClient HttpClient;

        public OpenAiEmbeddingProcessor(IOptions<OpenAiEmbeddingOptions> options, HttpClient? httpClient = null)
        {
            Options = options.Value;
            HttpClient = httpClient ?? new HttpClient();
        }

        public async Task<EmbeddingResponse?> GetDocumentEmbeddingsAsync(string text)
        {
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Add("api-key", Options.ApiKey);

            var requestBody = new { input = text };
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync(Options.Endpoint, content);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseStream = await response.Content.ReadAsStreamAsync();
            var embeddingResponse = await JsonSerializer.DeserializeAsync<EmbeddingResponse>(responseStream);

            return embeddingResponse;
        }
    }
}