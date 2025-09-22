namespace Google.GenerativeAI.Imagen40
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Output format for the generated image.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ImagenOutputFormat
    {
        [EnumMember(Value = "b64_json")]
        B64Json,

        [EnumMember(Value = "url")]
        Url
    }

    /// <summary>
    /// Aspect ratio for the generated image.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ImagenAspectRatio
    {
        [EnumMember(Value = "1:1")]
        Square_1x1,

        [EnumMember(Value = "9:16")]
        Portrait_9x16,

        [EnumMember(Value = "16:9")]
        Landscape_16x9
    }

    /// <summary>
    /// Safety filter strength.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ImagenSafetyFilterLevel
    {
        [EnumMember(Value = "block_most")]
        BlockMost,

        [EnumMember(Value = "block_some")]
        BlockSome,

        [EnumMember(Value = "block_few")]
        BlockFew
    }

    /// <summary>
    /// Controls the generation of people in images.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ImagenPersonGeneration
    {
        [EnumMember(Value = "allow_adult")]
        AllowAdult,

        [EnumMember(Value = "dont_allow")]
        DontAllow
    }
}