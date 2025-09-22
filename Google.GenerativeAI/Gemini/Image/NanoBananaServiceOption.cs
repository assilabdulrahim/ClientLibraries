namespace Google.GenerativeAI.Gemini.Image
{
    public class NanoBananaServiceOption
    {
        public string ApiKey { get; set; }
        public string ImageEndpoint { get; set; }
        public TimeSpan RequestTimeout { get; set; } = TimeSpan.FromMinutes(5);
        public string TextualEndpoint { get; set; }
    }
}