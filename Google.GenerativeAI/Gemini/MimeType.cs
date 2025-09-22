namespace Google.GenerativeAI.Gemini
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents the only allowed MIME types for generative AI requests and responses.
    /// </summary>
    /// <remarks>
    /// Only these values are supported by the API: <c>text/plain</c>, <c>application/json</c>, <c>application/xml</c>, <c>application/yaml</c>, and <c>text/x.enum</c>.
    /// </remarks>
    public enum MimeType
    {
        /// <summary>Plain text format ("text/plain").</summary>
        [EnumMember(Value = "text/plain")]
        TextPlain,

        /// <summary>JSON format ("application/json").</summary>
        [EnumMember(Value = "application/json")]
        ApplicationJson,

        /// <summary>XML format ("application/xml").</summary>
        [EnumMember(Value = "application/xml")]
        ApplicationXml,

        /// <summary>YAML format ("application/yaml").</summary>
        [EnumMember(Value = "application/yaml")]
        ApplicationYaml,

        /// <summary>Enum text format ("text/x.enum").</summary>
        [EnumMember(Value = "text/x.enum")]
        TextXEnum
    }
}