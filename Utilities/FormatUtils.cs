
using System.Globalization;

namespace WorkFlow_SIG10._1.Utilities
{
    public static class FormatUtils
    {
        private static readonly CultureInfo CustomCulture;

        static FormatUtils()
        {
            CustomCulture = new CultureInfo("es-CL"); 
            CustomCulture.NumberFormat.CurrencySymbol = "$";
            CustomCulture.NumberFormat.CurrencyGroupSeparator = ".";
            CustomCulture.NumberFormat.CurrencyDecimalSeparator = ",";
            CustomCulture.NumberFormat.NumberGroupSeparator = ".";
            CustomCulture.NumberFormat.NumberDecimalSeparator = ",";
            CustomCulture.NumberFormat.PercentSymbol = "%";
            CustomCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
        }

        public static string FormatCurrency(decimal value)
        {
            return value.ToString("C2", CustomCulture);
        }

        public static string FormatNumber(decimal value, int decimalPlaces = 2)
        {
             return value.ToString($"N{decimalPlaces}", CustomCulture);
        }

        public static string FormatDate(DateTime? date)
        {
            return date?.ToString("d", CustomCulture) ?? string.Empty;
        }

        public static string FormatPercentage(decimal value)
        {
            // The "P" format specifier multiplies the number by 100 and adds the culture's percent symbol.
            return value.ToString("P2", CustomCulture);
        }
    }
}
