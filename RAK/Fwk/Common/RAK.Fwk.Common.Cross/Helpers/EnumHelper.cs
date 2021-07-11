using System;
using System.Linq;
using System.Runtime.Serialization;

namespace RAK.Fwk.Common.Cross.Helpers
{
    /// <summary>
    /// Helper para Enumeracion
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Obtiene los EnumMember de un enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ToEnumString<T>(T type)
        {
            var enumType = typeof(T);
            var name = Enum.GetName(enumType, type);
            var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
            return enumMemberAttribute.Value;
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// Obtiene el valor de un enum a partir de un int
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TEnum GetEnumValue<TEnum>(int value)
        {
            return (TEnum)Enum.ToObject(typeof(TEnum), value);
        }
    }
}
