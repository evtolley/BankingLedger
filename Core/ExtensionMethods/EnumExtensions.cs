using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Core.ExtensionMethods
{
    // quick extension method for purposes of this code sample taken from 
    // https://codereview.stackexchange.com/questions/157871/method-that-returns-description-attribute-of-enum-value
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            return
                value
                    .GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DescriptionAttribute>()
                    ?.Description
                ?? value.ToString();
        }
    }
}
