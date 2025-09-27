namespace Google.GenerativeAI.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Safety categories for content filtering.
    /// </summary>
    public enum SafetySettingCategory
    {
        [EnumMember(Value = "HARM_CATEGORY_SEXUALLY_EXPLICIT")]
        HarmCategorySexuallyExplicit,

        [EnumMember(Value = "HARM_CATEGORY_HATE_SPEECH")]
        HarmCategoryHateSpeech,

        [EnumMember(Value = "HARM_CATEGORY_HARASSMENT")]
        HarmCategoryHarassment,

        [EnumMember(Value = "HARM_CATEGORY_DANGEROUS_CONTENT")]
        HarmCategoryDangerousContent
    }
}