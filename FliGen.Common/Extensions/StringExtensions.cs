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
    }
}