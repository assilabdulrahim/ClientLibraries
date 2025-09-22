namespace Google.GenerativeAI.Imagen40
{
    using Google.GenerativeAI.Imagen40.Response;

    public interface IImageGenerativeService
    {
        IImageGenerativeService WithPrompt(string prompt);

        IImageGenerativeService WithoutPrompt(string prompt);

        IImageGenerativeService WithSeed(int seed);

        IImageGenerativeService SetSampleCount(int sampleCount);

        IImageGenerativeService SetAspect(ImagenAspectRatio aspectRatio);

        IImageGenerativeService SetFormat(ImagenOutputFormat imagenOutputFormat);

        IImageGenerativeService SetSafetyFilter(ImagenSafetyFilterLevel imagenSafetyFilterLevel);

        IImageGenerativeService SetPersonsFilter(ImagenPersonGeneration imagenPersonGeneration);

        Task<ImagenGenerateResponse> GenerateImageAsync();
    }
}