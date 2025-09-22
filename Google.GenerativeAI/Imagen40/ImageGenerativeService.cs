namespace Google.GenerativeAI.Imagen40
{
    using Google.GenerativeAI.Imagen40.Response;
    using Microsoft.Extensions.Options;
    using Request;
    using System.Diagnostics;
    using System.Text.Json;

    public class ImageGenerativeService : IImageGenerativeService
    {
        private readonly ImagenGenerateRequest Request = new();
        private readonly HttpClient HttpClient;
        private readonly ImageGenerativeServiceOption Option;

        public ImageGenerativeService(IHttpClientFactory httpClientFactory, IOptions<ImageGenerativeServiceOption> option)
        {
            HttpClient = httpClientFactory.CreateClient();
            Option = option?.Value ?? throw new ArgumentNullException(nameof(option));
        }

        public IImageGenerativeService WithPrompt(string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt))
                throw new ArgumentException("Prompt must not be empty.", nameof(prompt));
            Request.Prompt = prompt.Trim();
            return this;
        }

        public IImageGenerativeService WithoutPrompt(string negativePrompt)
        {
            if (string.IsNullOrWhiteSpace(negativePrompt))
                throw new ArgumentException("Negative prompt must not be empty.", nameof(negativePrompt));
            Request.NegativePrompt = negativePrompt.Trim();
            return this;
        }

        public IImageGenerativeService WithSeed(int seed)
        {
            if (seed < 0 || seed > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(seed), "Seed must be between 0 and 2,147,483,647.");
            Request.Seed = seed;
            return this;
        }

        public IImageGenerativeService SetSampleCount(int sampleCount)
        {
            if (sampleCount < 1 || sampleCount > 4)
                throw new ArgumentOutOfRangeException(nameof(sampleCount), "SampleCount must be between 1 and 4.");
            Request.SampleCount = sampleCount;
            return this;
        }

        public IImageGenerativeService SetAspect(ImagenAspectRatio aspectRatio)
        {
            Request.AspectRatio = aspectRatio switch
            {
                ImagenAspectRatio.Square_1x1 => "1:1",
                ImagenAspectRatio.Portrait_9x16 => "9:16",
                ImagenAspectRatio.Landscape_16x9 => "16:9",
                _ => throw new ArgumentOutOfRangeException(nameof(aspectRatio), "Invalid aspect ratio.")
            };
            return this;
        }

        public IImageGenerativeService SetFormat(ImagenOutputFormat imagenOutputFormat)
        {
            Request.OutputFormat = imagenOutputFormat switch
            {
                ImagenOutputFormat.B64Json => "b64_json",
                ImagenOutputFormat.Url => "url",
                _ => throw new ArgumentOutOfRangeException(nameof(imagenOutputFormat), "Invalid output format.")
            };
            return this;
        }

        public IImageGenerativeService SetSafetyFilter(ImagenSafetyFilterLevel imagenSafetyFilterLevel)
        {
            Request.SafetyFilterLevel = imagenSafetyFilterLevel switch
            {
                ImagenSafetyFilterLevel.BlockMost => "block_most",
                ImagenSafetyFilterLevel.BlockSome => "block_some",
                ImagenSafetyFilterLevel.BlockFew => "block_few",
                _ => throw new ArgumentOutOfRangeException(nameof(imagenSafetyFilterLevel), "Invalid safety filter level.")
            };
            return this;
        }

        public IImageGenerativeService SetPersonsFilter(ImagenPersonGeneration imagenPersonGeneration)
        {
            Request.PersonGeneration = imagenPersonGeneration switch
            {
                ImagenPersonGeneration.AllowAdult => "allow_adult",
                ImagenPersonGeneration.DontAllow => "dont_allow",
                _ => throw new ArgumentOutOfRangeException(nameof(imagenPersonGeneration), "Invalid person generation value.")
            };
            return this;
        }

        public async Task<ImagenGenerateResponse> GenerateImageAsync()
        {
            if (string.IsNullOrWhiteSpace(Request.Prompt))
                throw new InvalidOperationException("Prompt is required before generating an image.");
            HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Option.ApiKey);
            HttpClient.BaseAddress = new Uri(Option.ApiUrl);
            var serializedRequest = JsonSerializer.Serialize(Request, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            Debug.WriteLine(Option.ApiUrl);
            Debug.WriteLine(serializedRequest);
            var content = new StringContent(serializedRequest, System.Text.Encoding.UTF8, "application/json");
            using var response = await HttpClient.PostAsync(string.Empty, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var imagenGenerateResponse = JsonSerializer.Deserialize<ImagenGenerateResponse>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? throw new InvalidOperationException("Failed to deserialize the response.");
            return imagenGenerateResponse;
        }
    }
}