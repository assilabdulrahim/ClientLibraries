namespace Google.GenerativeAI.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Thresholds for how strictly to block content.
    /// </summary>
    public enum SafetySettingThreshold
    {
        [EnumMember(Value = "HARM_THRESHOLD_VERY_UNLIKELY")]
        HarmThresholdVeryUnlikely,

        [EnumMember(Value = "HARM_THRESHOLD_UNLIKELY")]
        HarmThresholdUnlikely,

        [EnumMember(Value = "HARM_THRESHOLD_POSSIBLE")]
        HarmThresholdPossible,

        [EnumMember(Value = "HARM_THRESHOLD_LIKELY")]
        HarmThresholdLikely,

        [EnumMember(Value = "HARM_THRESHOLD_VERY_LIKELY")]
        HarmThresholdVeryLikely
    }
}