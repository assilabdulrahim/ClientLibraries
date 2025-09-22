namespace Google.GenerativeAI.Gemini.Textual.Request
{
    using Google.GenerativeAI.Extensions;
    using Google.GenerativeAI.Gemini;
    using Google.GenerativeAI.Gemini.Image;
    using Google.GenerativeAI.Gemini.Textual.Response;
    using Microsoft.Extensions.Options;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Reflection;
    using System.Threading.Tasks;

    public class TextualGeminiService : ITextualGeminiService
    {
        private readonly IHttpClientFactory HttpClientFactory;
        private readonly NanoBananaServiceOption Options;
        private GeminiRequest Request;

        public TextualGeminiService(IHttpClientFactory httpClientFactory, IOptions<NanoBananaServiceOption> options)
        {
            HttpClientFactory = httpClientFactory;
            Options = options.Value;
            Request = new GeminiRequest
            {
                Contents = new List<Content>()
            };
        }

        public string ErrorMessage { get; private set; }

        public ITextualGeminiService AddContent(ContentRole role, params Part[] parts)
        {
            Request.Contents ??= new List<Content>();
            Request.Contents.Add(new Content
            {
                Role = role.GetEnumMemberValue(),
                Parts = parts?.ToList() ?? new List<Part>()
            });
            return this;
        }

        public ITextualGeminiService AddFunctionDeclaration(FunctionDeclaration functionDeclaration)
        {
            if (functionDeclaration == null)
                throw new ArgumentNullException(nameof(functionDeclaration));
            Request.Tools ??= new List<Tool>();
            if (!Request.Tools.Any())
                Request.Tools.Add(new Tool { FunctionDeclarations = new List<FunctionDeclaration>() });
            Request.Tools[0].FunctionDeclarations ??= new List<FunctionDeclaration>();
            Request.Tools[0].FunctionDeclarations.Add(functionDeclaration);
            return this;
        }

        public ITextualGeminiService AddFunctionDeclarations(IEnumerable<FunctionDeclaration> functionDeclarations)
        {
            if (functionDeclarations == null)
                throw new ArgumentNullException(nameof(functionDeclarations));

            var declarationsList = functionDeclarations.ToList();
            if (declarationsList.Count == 0)
                throw new ArgumentException("At least one function declaration must be provided.", nameof(functionDeclarations));

            foreach (var fd in declarationsList)
            {
                if (fd == null)
                    throw new ArgumentException("Function declaration cannot be null.", nameof(functionDeclarations));
                if (string.IsNullOrWhiteSpace(fd.Name))
                    throw new ArgumentException("Function declaration must have a non-empty Name.", nameof(functionDeclarations));
                if (fd.Parameters == null)
                    throw new ArgumentException($"Function declaration '{fd.Name}' must have a Parameters schema.", nameof(functionDeclarations));
            }

            Request.Tools ??= new List<Tool>();
            // Always use a single Tool for all function declarations (Gemini API convention)
            Tool tool;
            if (Request.Tools.Count == 0)
            {
                tool = new Tool { FunctionDeclarations = new List<FunctionDeclaration>() };
                Request.Tools.Add(tool);
            }
            else
            {
                tool = Request.Tools[0];
                tool.FunctionDeclarations ??= new List<FunctionDeclaration>();
            }

            tool.FunctionDeclarations.AddRange(declarationsList);
            return this;
        }

        public ITextualGeminiService AddSafetySetting(SafetySettingCategory category, SafetySettingThreshold threshold)
        {
            Request.SafetySettings ??= new List<SafetySetting>();
            Request.SafetySettings.Add(new SafetySetting
            {
                Category = category.GetEnumMemberValue(),
                Threshold = threshold.GetEnumMemberValue()
            });
            return this;
        }

        public ITextualGeminiService AddTool(Tool tool)
        {
            Request.Tools ??= new List<Tool>();
            Request.Tools.Add(tool);
            return this;
        }

        public ITextualGeminiService ClearState()
        {
            Request = new GeminiRequest
            {
                Contents = new List<Content>()
            };
            return this;
        }

        public ITextualGeminiService ConfigureGenerationConfig(GenerationConfig config)
        {
            Request.GenerationConfig = config;
            return this;
        }

        public async Task<GeminiResponse> GenerateAsync()
        {
            var httpClient = HttpClientFactory.CreateClient();
            httpClient.Timeout = TimeSpan.FromMinutes(2);

            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName();
            var assemblyVersion = assemblyName.Version?.ToString() ?? "1.0.0";
            httpClient.DefaultRequestHeaders.Add("X-Client-Version", $"{assemblyName.Name}-{assemblyVersion}");
            httpClient.DefaultRequestHeaders.Add("x-goog-api-key", Options.ApiKey);

            var apiResponse = await httpClient.PostAsJsonAsync(Options.TextualEndpoint, Request);

            if (!apiResponse.IsSuccessStatusCode)
            {
                var errorContent = await apiResponse.Content.ReadAsStringAsync();
                ErrorMessage = $"API Error: {apiResponse.ReasonPhrase}. Details: {errorContent}";
                throw new Exception(ErrorMessage);
            }

            var response = await apiResponse.Content.ReadFromJsonAsync<GeminiResponse>();
            return response;
        }

        public ITextualGeminiService SetCandidateCount(int candidateCount)
        {
            if (candidateCount < 1)
                throw new ArgumentOutOfRangeException(nameof(candidateCount), "CandidateCount must be at least 1.");
            Request.GenerationConfig ??= new GenerationConfig();
            Request.GenerationConfig.CandidateCount = candidateCount;
            return this;
        }

        public ITextualGeminiService SetMaxOutputTokens(int maxOutputTokens)
        {
            if (maxOutputTokens < 1)
                throw new ArgumentOutOfRangeException(nameof(maxOutputTokens), "MaxOutputTokens must be at least 1.");
            Request.GenerationConfig ??= new GenerationConfig();
            Request.GenerationConfig.MaxOutputTokens = maxOutputTokens;
            return this;
        }

        public ITextualGeminiService SetStopSequences(params string[] stopSequences)
        {
            Request.GenerationConfig ??= new GenerationConfig();
            Request.GenerationConfig.StopSequences = stopSequences?.ToList() ?? new List<string>();
            return this;
        }

        public ITextualGeminiService SetSystemInstruction(Content systemInstruction)
        {
            Request.SystemInstruction = systemInstruction;
            return this;
        }

        public ITextualGeminiService SetSystemInstructionParts(params Part[] parts)
        {
            if (parts == null || parts.Length == 0)
                throw new ArgumentException("At least one part must be provided.", nameof(parts));
            Request.SystemInstruction ??= new Content();
            Request.SystemInstruction.Parts = parts.ToList();
            return this;
        }

        public ITextualGeminiService SetSystemInstructionRole(ContentRole role)
        {
            Request.SystemInstruction ??= new Content();
            Request.SystemInstruction.Role = role.GetEnumMemberValue();
            return this;
        }

        public ITextualGeminiService SetSystemInstructionText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("System instruction text must not be empty.", nameof(text));
            Request.SystemInstruction = new Content
            {
                Parts = new List<Part> { new Part { Text = text } },
                Role = ContentRole.User.GetEnumMemberValue() // or another default role
            };
            return this;
        }

        public ITextualGeminiService SetTemperature(float temperature)
        {
            if (temperature < 0.0f || temperature > 1.0f)
                throw new ArgumentOutOfRangeException(nameof(temperature), "Temperature must be between 0.0 and 1.0.");
            Request.GenerationConfig ??= new GenerationConfig();
            Request.GenerationConfig.Temperature = temperature;
            return this;
        }

        public ITextualGeminiService SetTopK(int topK)
        {
            if (topK < 1)
                throw new ArgumentOutOfRangeException(nameof(topK), "TopK must be at least 1.");
            Request.GenerationConfig ??= new GenerationConfig();
            Request.GenerationConfig.TopK = topK;
            return this;
        }

        public ITextualGeminiService SetTopP(float topP)
        {
            if (topP < 0.0f || topP > 1.0f)
                throw new ArgumentOutOfRangeException(nameof(topP), "TopP must be between 0.0 and 1.0.");
            Request.GenerationConfig ??= new GenerationConfig();
            Request.GenerationConfig.TopP = topP;
            return this;
        }

        public ITextualGeminiService WithPrompt(string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt))
                throw new ArgumentException("Prompt must not be empty.", nameof(prompt));

            if (Request.Contents == null || !Request.Contents.Any())
                Request.Contents = new List<Content> { new Content { Parts = new List<Part>() } };

            var content = Request.Contents[0];
            content.Parts ??= new List<Part>();
            content.Parts.Add(new Part { Text = prompt });

            return this;
        }

        public ITextualGeminiService WithRole(ContentRole role)
        {
            if (Request.Contents == null || !Request.Contents.Any())
                Request.Contents = new List<Content> { new Content { Parts = new List<Part>() } };

            Request.Contents[0].Role = role.GetEnumMemberValue();
            return this;
        }
    }
}