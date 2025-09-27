namespace Ai.Fluent.Core.DataContracts
{
    using System.Text.Json.Serialization;

    public partial class Payload
    {
        public class Part
        {
            [JsonPropertyName("text")]
            public string Text { get; set; }
        }
    }
}