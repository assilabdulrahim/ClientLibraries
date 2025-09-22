namespace Google.GenerativeAI.Imagen40
{
    public class ImageGenerativeServiceOption
    {
        public string ApiKey { get; set; }
        public string ApiUrl { get; set; }
        public TimeSpan RequestTimeout { get; set; } = TimeSpan.FromMinutes(5);
    }
}