using Google.GenerativeAI.Gemini.Textual.Response;

namespace Google.GenerativeAI.Gemini.Textual.Request
{
    public interface ITextualGeminiService
    {
        ITextualGeminiService AddContent(ContentRole role, params Part[] parts);

        ITextualGeminiService AddFunctionDeclarations(IEnumerable<FunctionDeclaration> functionDeclarations);

        ITextualGeminiService AddSafetySetting(SafetySettingCategory category, SafetySettingThreshold threshold);

        ITextualGeminiService ClearState();

        Task<GeminiResponse> GenerateAsync();

        ITextualGeminiService SetCandidateCount(int candidateCount);

        ITextualGeminiService SetMaxOutputTokens(int maxOutputTokens);

        ITextualGeminiService SetStopSequences(params string[] stopSequences);

        ITextualGeminiService SetSystemInstructionParts(params Part[] parts);

        ITextualGeminiService SetSystemInstructionRole(ContentRole role);

        // System instruction configuration
        ITextualGeminiService SetSystemInstructionText(string text);

        // GenerationConfig breakdown
        ITextualGeminiService SetTemperature(float temperature);

        ITextualGeminiService SetTopK(int topK);

        ITextualGeminiService SetTopP(float topP);

        ITextualGeminiService WithPrompt(string prompt);

        ITextualGeminiService WithRole(ContentRole role);
    }
}