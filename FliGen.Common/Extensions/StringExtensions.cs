using System.Linq;

namespace FliGen.Common.Extensions
{
    public static class StringExtensions
    {
        public static string CommaToDot(this string str)
        {
            var index = str.IndexOf(',');
            if (index == -1)
            {
                return str;
            }

            return string.Concat(str.Substring(0, index), '.', str.Substring(index + 1));
        }

        public static string Underscore(this string value)
        {
            return string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString()));
        }
    }
}