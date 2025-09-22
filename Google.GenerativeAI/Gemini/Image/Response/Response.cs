namespace Google.GenerativeAI.Gemini.Image.Response
{
    using Google.GenerativeAI.Gemini.Image.Request;
    using System.Collections.Generic;

    public class GenerateImageResponse
    {
        public List<Candidate> Candidates { get; set; }
        public PromptFeedback PromptFeedback { get; set; }
        public UsageMetadata UsageMetadata { get; set; }
    }

    public class Candidate
    {
        public Content Content { get; set; }
        public string FinishReason { get; set; }
        public List<SafetyRating> SafetyRatings { get; set; }
        public CitationMetadata CitationMetadata { get; set; }
        public int? TokenCount { get; set; }
        public int? Index { get; set; }
    }

    public class SafetyRating
    {
        public string Category { get; set; }
        public string Probability { get; set; }
        public bool? Blocked { get; set; }
    }

    public class CitationMetadata
    {
        public List<CitationSource> CitationSources { get; set; }
    }

    public class CitationSource
    {
        public int? StartIndex { get; set; }
        public int? EndIndex { get; set; }
        public string Uri { get; set; }
        public string License { get; set; }
    }

    public class PromptFeedback
    {
        public string BlockReason { get; set; }
        public List<SafetyRating> SafetyRatings { get; set; }
    }

    public class UsageMetadata
    {
        public int? PromptTokenCount { get; set; }
        public int? CandidatesTokenCount { get; set; }
        public int? TotalTokenCount { get; set; }
    }
}