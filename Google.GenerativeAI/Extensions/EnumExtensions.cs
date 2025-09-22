namespace Google.GenerativeAI.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;

    public static class EnumExtensions
    {
        public static string GetEnumMemberValue<T>(this T enumValue) where T : Enum
        {
            var type = typeof(T);
            var member = type.GetMember(enumValue.ToString()).FirstOrDefault();
            var attr = member?.GetCustomAttribute<EnumMemberAttribute>();
            return attr?.Value ?? enumValue.ToString();
        }
    }
}