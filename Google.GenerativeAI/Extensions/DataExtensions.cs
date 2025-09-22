namespace Google.GenerativeAI.Extensions
{
    using Google.GenerativeAI.Gemini.Image.Request;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats.Png;
    using SixLabors.ImageSharp.PixelFormats;
    using System.Text.Json;

    public static class DataExtensions
    {
        public static string Serialize(this object @object)
        {
            return JsonSerializer.Serialize(@object, new JsonSerializerOptions { WriteIndented = true });
        }

        public static T Deserialize<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { WriteIndented = true });
        }

        public static void SaveImageAs(this InlineData inlineData, string imageName)

        {
            byte[] imageBytes = Convert.FromBase64String(inlineData.Data);
            using (var ms = new MemoryStream(imageBytes))
            using (var image = Image.Load<Rgba32>(ms))
            {
                image.Save($"{imageName}.png", new PngEncoder());
            }
        }

        public static string ToBase64Png(this Image<Rgba32> image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, new PngEncoder());
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}