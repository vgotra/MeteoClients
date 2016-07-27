using System;
using System.Globalization;

namespace MeteoClients.Extensions
{
    public static class FormattingExtensions
    {
        public static string Invariant(this FormattableString formattable)
        {
            return formattable?.ToString(CultureInfo.InvariantCulture);
        }

        public static string Invariant(this float value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
