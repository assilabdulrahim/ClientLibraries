namespace Google.GenerativeAI.Gemini
{
    using Google.GenerativeAI.Contracts;
    using Google.GenerativeAI.Gemini.GenerativeData;
    using System.Text;
    using System.Threading.Tasks;

    public sealed class GeminiClient : IGeminiClient
    {
        private readonly HttpClient HttpClient;
        private readonly string GeminiApiUrl;
        private readonly string ApiKey;

        public GeminiClient(HttpClient httpClient, string geminiApiUrl, string apiKey)
        {
            HttpClient = httpClient;
            GeminiApiUrl = geminiApiUrl;
            ApiKey = apiKey;
        }

        public async Task<string?> GetExpandedConceptsAsync(string prompt)
        {
            var geminiRequest = new GeminiRequest
            {
                Contents = new List<GeminiContent>
                {
                    new GeminiContent
                    {
                        Parts = new List<GeminiPart>
                        {
                            new GeminiPart { Text = prompt }
                        }
                    }
                },
                SystemInstruction = new GeminiSystemInstruction
                {
                    Parts = new List<GeminiPart>
                    {
                        new GeminiPart { Text = "Expand the concepts in the prompt." }
                    }
                }
            };

            var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(geminiRequest),
                Encoding.UTF8,
                "application/json");

            using var request = new HttpRequestMessage(HttpMethod.Post, GeminiApiUrl)
            {
                Content = content
            };

            // If your Gemini API requires an API key in the header
            request.Headers.Add("x-goog-api-key", $"{ApiKey}");

            using var response = await HttpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return null;

            var responseString = await response.Content.ReadAsStringAsync();

            // Assume the response is a JSON object: { "expandedConcepts": "..." }
            using var doc = System.Text.Json.JsonDocument.Parse(responseString);
            if (doc.RootElement.TryGetProperty("expandedConcepts", out var conceptsElement))
            {
                return conceptsElement.GetString();
            }

            // Fallback: just return the whole response as string
            // Parse the Gemini response and extract the expanded concepts from the first candidate's first part
            var geminiResponse = System.Text.Json.JsonSerializer.Deserialize<GeminiResponse>(responseString);

            var expandedConcepts = geminiResponse?
                .Candidates?
                .FirstOrDefault()?
                .Content?
                .Parts?
                .FirstOrDefault()?
                .Text;

            return expandedConcepts ?? responseString;
        }
    }
}