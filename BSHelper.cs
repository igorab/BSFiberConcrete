using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    public class BSHelper
    {
        public const string UnitLength = "мм";

        public static double Kg2T(double _kg) => _kg * 0.0001d; 
        public static double MPA2kgsm2(double _mpa) => 10.197162d * _mpa;

        public static string EnumDescription(Enum myEnumVariable)
        {
            string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;
            return desc; 
        }
    }

    public static class EnumHelper
    {
        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        /// <example><![CDATA[string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;]]></example>
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }
    }


}
