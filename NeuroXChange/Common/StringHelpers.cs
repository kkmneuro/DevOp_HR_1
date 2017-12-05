using System;
using System.Globalization;

namespace NeuroXChange.Common
{
    public static class StringHelpers
    {
        public static double ParseDoubleCultureIndependent(string value)
        {
            double result;

            //Try parsing in the current culture
            if (double.TryParse(value, System.Globalization.NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out result) ||
                //Then in neutral language
                double.TryParse(value, System.Globalization.NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result) ||
                //Then in french language
                double.TryParse(value, System.Globalization.NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("fr-FR"), out result))
            {
                return result;
            }

            throw new Exception("Can't parse float value of the string!");
        }
    }
}
