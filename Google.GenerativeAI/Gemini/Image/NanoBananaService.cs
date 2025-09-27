namespace Google.GenerativeAI.Gemini.Image
{
    using Google.GenerativeAI.Enums;
    using Google.GenerativeAI.Extensions;
    using Google.GenerativeAI.Gemini.Image.Request;
    using Google.GenerativeAI.Gemini.Image.Response;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Reflection;

    /// <summary>
    /// Provides a fluent interface for configuring and generating images using the Gemini API.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This service allows you to build up a request for the Gemini API by chaining configuration methods.
    /// You can set prompts, generation parameters, safety settings, function declarations, and more.
    /// </para>
    /// <para>
    /// <b>Usage Example:</b>
    /// <code>
    /// var result = await service
    ///     .WithPrompt("A cat riding a bicycle")
    ///     .ConfigureTemperature(0.7f)
    ///     .ConfigureMimeType(MimeType.ImagePng)
    ///     .AddSafetySettings(SafetySettingCategory.HarmCategoryHarassment, SafetySettingThreshold.HarmThresholdUnlikely)
    ///     .Generate();
    /// </code>
    /// </para>
    /// <para>
    /// <b>Thread Safety:</b> This class is not thread-safe. Use a new instance per request.
    /// </para>
    /// </remarks>
    public class NanoBananaService : INanoBananaService
    {
        /// <summary>
        /// The HTTP client factory used to create HTTP clients for API requests.
        /// </summary>
        private readonly IHttpClientFactory HttpClientFactory;

        /// <summary>
        /// The options for the NanoBananaService, including API key and URL.
        /// </summary>
        private readonly NanoBananaServiceOption NanoBananaServiceOption;

        /// <summary>
        /// The current image generation request being built.
        /// </summary>
        private InstancePayload GenerateImageRequest;

        /// <summary>
        /// Initializes a new instance of the <see cref="NanoBananaService"/> class.
        /// </summary>
        /// <param name="httpClientFactory">The HTTP client factory.</param>
        /// <param name="options">The options for the service, including API key and URL.</param>
        public NanoBananaService(IHttpClientFactory httpClientFactory, IOptions<NanoBananaServiceOption> options)
        {
            GenerateImageRequest = new InstancePayload();
            GenerateImageRequest.Contents = new List<Content>();
            HttpClientFactory = httpClientFactory;
            NanoBananaServiceOption = options.Value;
        }

        /// <summary>
        /// Gets the last error message, if any, from the most recent API call.
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <inheritdoc/>
        public INanoBananaService AddFunction(string name, string description, IEnumerable<Schema> parameters = null)
        {
            GenerateImageRequest = new InstancePayload();
            GenerateImageRequest.Tools ??= new List<Tool>();

            // Always use a single Tool for all function declarations
            var tool = GenerateImageRequest.Tools.FirstOrDefault();
            if (tool == null)
            {
                tool = new Tool { FunctionDeclarations = new List<FunctionDeclaration>() };
                GenerateImageRequest.Tools.Add(tool);
            }
            tool.FunctionDeclarations ??= new List<FunctionDeclaration>();

            tool.FunctionDeclarations.Add(new FunctionDeclaration
            {
                Name = name,
                Description = description,
                Parameters = parameters?.FirstOrDefault()
            });

            return this;
        }

        /// <inheritdoc/>
        public INanoBananaService AddSafetySettings(SafetySettingCategory safetySettingCategory, SafetySettingThreshold safetySettingThreshold)
        {
            GenerateImageRequest = new InstancePayload();
            GenerateImageRequest.SafetySettings ??= new List<SafetySetting>();
            GenerateImageRequest.SafetySettings.Add(new SafetySetting
            {
                Category = safetySettingCategory.GetEnumMemberValue(),
                Threshold = safetySettingThreshold.GetEnumMemberValue()
            });
            return this;
        }

        /// <inheritdoc/>
        public INanoBananaService ConfigureMaxTokens(int maxTokens)
        {
            GenerateImageRequest = new InstancePayload();
            GenerateImageRequest.GenerationConfig ??= new GenerationConfig();
            GenerateImageRequest.GenerationConfig.MaxOutputTokens = maxTokens;
            return this;
        }

        /// <inheritdoc/>
        public INanoBananaService ConfigureMimeType(MimeType mimeType)
        {
            GenerateImageRequest = new InstancePayload();
            GenerateImageRequest.GenerationConfig ??= new GenerationConfig();

            var mimeTypeString = mimeType.GetEnumMemberValue();
            GenerateImageRequest.GenerationConfig.ResponseMimeType = mimeTypeString;
            return this;
        }

        /// <inheritdoc/>
        public INanoBananaService ConfigureResponseMimeType(string mimeType)
        {
            GenerateImageRequest = new InstancePayload();
            GenerateImageRequest.GenerationConfig ??= new GenerationConfig();
            GenerateImageRequest.GenerationConfig.ResponseMimeType = mimeType;
            return this;
        }

        /// <inheritdoc/>
        public INanoBananaService ConfigureStopSequence(string[] stopSequence)
        {
            GenerateImageRequest = new InstancePayload();
            GenerateImageRequest.GenerationConfig ??= new GenerationConfig();
            GenerateImageRequest.GenerationConfig.StopSequences = stopSequence?.ToList() ?? new List<string>();
            return this;
        }

        /// <inheritdoc/>
        public INanoBananaService ConfigureTemperature(float temperature)
        {
            GenerateImageRequest = new InstancePayload();
            GenerateImageRequest.GenerationConfig ??= new GenerationConfig();
            GenerateImageRequest.GenerationConfig.Temperature = temperature;
            return this;
        }

        /// <inheritdoc/>
        public INanoBananaService ConfigureTopK(int topK)
        {
            GenerateImageRequest = new InstancePayload();
            GenerateImageRequest.GenerationConfig ??= new GenerationConfig();
            GenerateImageRequest.GenerationConfig.TopK = topK;
            return this;
        }

        /// <inheritdoc/>
        public INanoBananaService ConfigureTopP(float topP)
        {
            GenerateImageRequest = new InstancePayload();
            GenerateImageRequest.GenerationConfig ??= new GenerationConfig();
            GenerateImageRequest.GenerationConfig.TopP = topP;
            return this;
        }

        /// <summary>
        /// Adds a new content entry with the specified role and parts to the request.
        /// </summary>
        /// <param name="role">The role to assign (e.g., user, model, function, tool).</param>
        /// <param name="parts">A list of parts (text, inlineData, fileData, etc.) for this content.</param>
        /// <returns>The current <see cref="NanoBananaService"/> instance.</returns>
        public INanoBananaService AddContent(ContentRole role, IEnumerable<Part> parts)
        {
            GenerateImageRequest = new InstancePayload();
            GenerateImageRequest.Contents ??= new List<Content>();

            GenerateImageRequest.Contents.Add(new Content
            {
                Role = role.GetEnumMemberValue(),
                Parts = parts?.ToList() ?? new List<Part>()
            });

            return this;
        }

        /// <inheritdoc/>
        public async Task<InlineData> Generate()
        {
            var httpClient = HttpClientFactory.CreateClient();
            httpClient.Timeout = TimeSpan.FromMinutes(5);
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName();
            var assemblyVersion = assemblyName.Version?.ToString() ?? "1.0.0";

            httpClient.DefaultRequestHeaders.Add("X-Client-Version", $"{assemblyName.Name}-{assemblyVersion}");
            httpClient.DefaultRequestHeaders.Add("x-goog-api-key", NanoBananaServiceOption.ApiKey);
            var serizliedRequest = System.Text.Json.JsonSerializer.Serialize(GenerateImageRequest, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            Debug.WriteLine($"Request: {serizliedRequest}");
            var apiResponse = await httpClient.PostAsJsonAsync(NanoBananaServiceOption.ImageEndpoint, GenerateImageRequest);

            if (!apiResponse.IsSuccessStatusCode)
            {
                var errorContent = await apiResponse.Content.ReadAsStringAsync();
                ErrorMessage = $"API Error: {apiResponse.ReasonPhrase}. Details: {errorContent}";
                throw new Exception(ErrorMessage);
            }

            var imageResponse = await apiResponse.Content.ReadFromJsonAsync<GenerateImageResponse>();

            var imageData =
                imageResponse?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.InlineData
                ??
                imageResponse?.Candidates?.FirstOrDefault()?.Content?.Parts?.LastOrDefault()?.InlineData;
            return imageData;
        }

        /// <summary>
        /// Optionally specify a path to save the generated image.
        /// </summary>
        /// <param name="newPathForTheImage">The file path to save the image.</param>
        /// <returns>The current <see cref="NanoBananaService"/> instance.</returns>
        public INanoBananaService SaveAs(string newPathForTheImage)
        {
            // Assuming SaveAs modifies the request in some way.
            return this;
        }

        /// <summary>
        /// Sets the prompt text for the request. Multiple calls will add multiple parts to the first content.
        /// </summary>
        /// <param name="prompt">The prompt text.</param>
        /// <returns>The current <see cref="NanoBananaService"/> instance.</returns>
        public INanoBananaService WithPrompt(string prompt)
        {
            // Defensive: ensure GenerateImageRequest is not null
            GenerateImageRequest = new InstancePayload();

            // Ensure at least one Content exists
            if (GenerateImageRequest.Contents == null || GenerateImageRequest.Contents.Count == 0)
            {
                GenerateImageRequest.Contents = new List<Content>
                        {
                            new Content { Parts = new List<Part>() }
                        };
            }

            // Use the first Content object
            var content = GenerateImageRequest.Contents[0];

            // Initialize Parts if null
            content.Parts ??= new List<Part>();

            // Add the new part
            content.Parts.Add(new Part { Text = prompt });

            return this;
        }

        /// <summary>
        /// Sets or updates the role for the first content in the request.
        /// If no content exists, a new one is created with the specified role.
        /// </summary>
        /// <param name="role">The role to assign (e.g., "user", "model", "system").</param>
        /// <returns>The current <see cref="NanoBananaService"/> instance.</returns>
        public INanoBananaService WithRole(ContentRole role)
        {
            GenerateImageRequest = new InstancePayload();
            if (GenerateImageRequest.Contents == null || GenerateImageRequest.Contents.Count == 0)
            {
                GenerateImageRequest.Contents = new List<Content>
                    {
                        new Content { Role = role.GetEnumMemberValue(), Parts = new List<Part>() }
                    };
            }
            else
            {
                GenerateImageRequest.Contents[0].Role = role.GetEnumMemberValue();
            }
            return this;
        }
    }
}