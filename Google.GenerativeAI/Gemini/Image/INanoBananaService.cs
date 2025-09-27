namespace Google.GenerativeAI.Gemini.Image
{
    using Google.GenerativeAI.Enums;
    using Google.GenerativeAI.Gemini.Image.Request;

    /// <summary>
    /// Provides a fluent interface for configuring and generating images using the Gemini API.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Parameter Explanations:</b>
    /// </para>
    /// <para>
    /// <b>Contents</b>: The main input to the model. It's an array of <see cref="Content"/> objects representing the conversation history.
    /// <list type="bullet">
    ///   <item>
    ///     <term>role</term>
    ///     <description>Specifies who said what. Use "user" for prompts or "model" for previous AI responses. This helps the model understand context.</description>
    ///   </item>
    ///   <item>
    ///     <term>parts</term>
    ///     <description>An array containing the different pieces of your input.</description>
    ///   </item>
    ///   <item>
    ///     <term>text</term>
    ///     <description>The actual text of your prompt or message.</description>
    ///   </item>
    /// </list>
    /// </para>
    /// <para>
    /// <b>Tools</b>: Allows you to define external functions the model can call (e.g., get weather, search a database).
    /// <list type="bullet">
    ///   <item>
    ///     <term>functionDeclarations</term>
    ///     <description>An array describing each function.</description>
    ///   </item>
    ///   <item>
    ///     <term>name</term>
    ///     <description>The function's name.</description>
    ///   </item>
    ///   <item>
    ///     <term>description</term>
    ///     <description>A clear explanation of what the function does.</description>
    ///   </item>
    ///   <item>
    ///     <term>parameters</term>
    ///     <description>Defines the function's inputs using a JSON schema (type, properties, required fields).</description>
    ///   </item>
    /// </list>
    /// </para>
    /// <para>
    /// <b>SafetySettings</b>: Controls how the model handles potentially harmful content.
    /// <list type="bullet">
    ///   <item>
    ///     <term>category</term>
    ///     <description>The type of harmful content to filter. Categories: <see cref="SafetySettingCategory"/>.</description>
    ///   </item>
    ///   <item>
    ///     <term>threshold</term>
    ///     <description>How strictly to block content. Options (from most to least restrictive): <see cref="SafetySettingThreshold"/>.</description>
    ///   </item>
    /// </list>
    /// </para>
    /// <para>
    /// <b>GenerationConfig</b>: Settings that control how the model generates its response.
    /// <list type="bullet">
    ///   <item>
    ///     <term>temperature</term>
    ///     <description>(0.0 - 1.0) Controls randomness. Higher = more creative, lower = more predictable.</description>
    ///   </item>
    ///   <item>
    ///     <term>topP</term>
    ///     <description>(0.0 - 1.0) Controls diversity by sampling from the most likely next words.</description>
    ///   </item>
    ///   <item>
    ///     <term>topK</term>
    ///     <description>(int) Limits choices to the K most likely next words.</description>
    ///   </item>
    ///   <item>
    ///     <term>maxOutputTokens</term>
    ///     <description>(int) Maximum number of tokens the model can generate.</description>
    ///   </item>
    ///   <item>
    ///     <term>stopSequences</term>
    ///     <description>Array of strings. Generation stops if any are encountered.</description>
    ///   </item>
    ///   <item>
    ///     <term>responseMimeType</term>
    ///     <description>The format of the response (e.g., "text/plain").</description>
    ///   </item>
    /// </list>
    /// </para>
    /// </remarks>
    public partial interface INanoBananaService
    {
        /// <summary>
        /// Gets the last error message, if any.
        /// </summary>
        string ErrorMessage { get; }

        INanoBananaService AddContent(ContentRole role, IEnumerable<Part> parts);

        INanoBananaService AddFunction(string name, string description, IEnumerable<Schema> parameters = null);

        /// <summary>
        /// Adds a safety setting to control content filtering.
        /// </summary>
        /// <param name="safetySettingCategory">The category of harmful content to filter.</param>
        /// <param name="safetySettingThreshold">How strictly to block content.</param>
        /// <returns>The current <see cref="INanoBananaService"/> instance.</returns>
        INanoBananaService AddSafetySettings(SafetySettingCategory safetySettingCategory, SafetySettingThreshold safetySettingThreshold);

        /// <summary>
        /// Configures the maximum number of output tokens.
        /// </summary>
        /// <param name="maxTokens">The maximum number of tokens.</param>
        /// <returns>The current <see cref="INanoBananaService"/> instance.</returns>
        INanoBananaService ConfigureMaxTokens(int maxTokens);

        /// <summary>
        /// Configures the response MIME type (e.g., "image/png").
        /// </summary>
        /// <param name="mimeType">The MIME type for the response.</param>
        /// <returns>The current <see cref="INanoBananaService"/> instance.</returns>
        INanoBananaService ConfigureResponseMimeType(string mimeType);

        INanoBananaService ConfigureMimeType(MimeType mimeType);

        /// <summary>
        /// Configures stop sequences. Generation will stop if any of these sequences are encountered.
        /// </summary>
        /// <param name="stopSequence">An array of stop sequences.</param>
        /// <returns>The current <see cref="INanoBananaService"/> instance.</returns>
        INanoBananaService ConfigureStopSequence(string[] stopSequence);

        /// <summary>
        /// Configures the temperature (randomness) for generation (0.0 - 1.0).
        /// </summary>
        /// <param name="temperature">A value between 0.0 and 1.0.</param>
        /// <returns>The current <see cref="INanoBananaService"/> instance.</returns>
        INanoBananaService ConfigureTemperature(float temperature);

        /// <summary>
        /// Configures the topK (limits choices to the K most likely next words).
        /// </summary>
        /// <param name="topK">The maximum number of choices (K).</param>
        /// <returns>The current <see cref="INanoBananaService"/> instance.</returns>
        INanoBananaService ConfigureTopK(int topK);

        /// <summary>
        /// Configures the topP (diversity) for generation (0.0 - 1.0).
        /// </summary>
        /// <param name="topP">A value between 0.0 and 1.0.</param>
        /// <returns>The current <see cref="INanoBananaService"/> instance.</returns>
        INanoBananaService ConfigureTopP(float topP);

        /// <summary>
        /// Sends the configured request to the Gemini API and returns the generated image data.
        /// </summary>
        /// <returns>The generated image data as <see cref="InlineData"/>.</returns>
        Task<InlineData> Generate();

        /// <summary>
        /// Optionally specify a path to save the generated image.
        /// </summary>
        /// <param name="newPathForTheImage">The file path to save the image.</param>
        /// <returns>The current <see cref="INanoBananaService"/> instance.</returns>
        INanoBananaService SaveAs(string newPathForTheImage);

        /// <summary>
        /// Sets the prompt text for the request.
        /// </summary>
        /// <param name="prompt">The prompt text.</param>
        /// <returns>The current <see cref="INanoBananaService"/> instance.</returns>
        INanoBananaService WithPrompt(string prompt);

        INanoBananaService WithRole(ContentRole role);
    }
}