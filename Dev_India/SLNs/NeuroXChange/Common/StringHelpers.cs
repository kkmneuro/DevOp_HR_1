using System;
using System.Globalization;

namespace NeuroXChange.Common
{
    public static class StringHelpers
    {
        public static double ParseDoubleCultureIndependent(string value, bool allowSign = false)
        {
            double result;

            NumberStyles style = NumberStyles.AllowDecimalPoint;
            if (allowSign)
            {
                style |= NumberStyles.AllowLeadingSign;
            }

            //Try parsing in the current culture
            if (double.TryParse(value, style, CultureInfo.CurrentCulture, out result) ||
                //Then in neutral language
                double.TryParse(value, style, CultureInfo.InvariantCulture, out result) ||
                //Then in french language
                double.TryParse(value, style, CultureInfo.GetCultureInfo("fr-FR"), out result))
            {
                return result;
            }

            throw new Exception("Can't parse float value of the string!");
        }

        public static bool TryParseDoubleCultureIndependent(string value, out double result)
        {
            result = 0.0;
            try
            {
                result = ParseDoubleCultureIndependent(value);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static string NullableToString<T> (T? a) where T : struct
        {
            return a.HasValue ? a.ToString() : "NULL";
        }
    }
}
